<template>
  <section>
    <!--工具条-->
    <el-col :span="24" class="toolbar">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input v-model="filters.name" placeholder="机构名称"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="queryClick" size="medium">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="add" size="medium">新增</el-button>
        </el-form-item>
      </el-form>
    </el-col>
    <!--列表-->
    <el-table :data="ous" highlight-current-row v-loading="listLoading" stripe border fit @selection-change="select">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column type="index" label="序号" width="80"></el-table-column>
      <el-table-column label="操作" width="160">
        <template slot-scope="scope">
          <el-button size="mini" @click="edit(scope.$index)">编辑</el-button>
          <el-button size="mini" @click="del(scope.$index)" type="danger">删除</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="Name" label="机构名称" width="200"></el-table-column>
      <el-table-column prop="Address" label="机构地址" width="250"></el-table-column>
      <el-table-column prop="Description" label="描述"></el-table-column>
    </el-table>
    <!--工具条-->
    <el-col :span="24" class="toolbar">
      <el-button type="danger" @click="delMulti" :disabled="this.selIds.length===0" size="medium">批量删除</el-button>
      <el-pagination layout="prev, pager, next" :page-size="20" @current-change="pageIndexChange" :total="ous.length" style="float:right;"></el-pagination>
    </el-col>

    <!-- 新增、编辑界面 -->
    <el-dialog :title="editTitle" :visible.sync="editVisible" :close-on-click-modal="false">
      <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
        <el-form-item label="机构名称" prop="Name">
          <el-input v-model="editData.Name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="机构地址" prop="Address">
          <el-input v-model="editData.Address" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="描述" prop="Description">
          <el-input v-model="editData.Description" auto-complete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editCancer">取消</el-button>
        <el-button type="primary" @click="editConfirm">提交</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import data from "../../commom/data.js";
import commom from "../../commom/commom.js";

export default {
  data() {
    return {
      ous: [],
      filters: {
        name: "",
        pageIndex: 1
      },
      selIds: [], // 列表选中列id
      users: [],
      total: 0,
      page: 1,
      listLoading: false,
      sels: [], //列表选中列
      // 编辑
      editTitle: "",
      editVisible: false,
      editData: {},
      editRule: {
        Name: [{ required: true, message: "请输入机构名称", trigger: ["blur"] }],
        Address: [{ required: true, message: "请输入机构名称", trigger: ["blur"] }],
        Description: [{ required: true, message: "请输入机构名称", trigger: ["blur"] }],
        
      }
    };
  },
  methods: {
    select: function(selection) {
      this.selIds = selection;
    },
    queryClick() {
      // this.ou = data.ou.filter(data => {
      //   return data.name.indexOf(this.filters.name) > -1;
      // });
    },
    pageIndexChange() {
      // this.ou = data.ou.filter(data => {
      //   return data.name.indexOf(this.filters.name) > -1;
      // });
    },
    // 新增、编辑
    add() {
      this.editTitle = "新增机构";
      this.editVisible = true;
      this.editData = {
        Name: "",
        Address: "",
        Description: ""
      };
    },
    edit(index) {
      this.editData = this.ous[index];
      this.editData.index = index;

      this.editTitle = "编辑机构";
      this.editVisible = true;
    },
    editConfirm() {
      this.$refs["editData"].validate(valid => {
        if (valid) {
debugger;
          if (this.editData.ID != null) {
            // 编辑
            this.$ajax.put("ou", this.editData).then(res => {
              
              console.log(res.data);
              this.editVisible = false;
            });
            commom.success("修改成功！");
            this.editVisible = false;
            // commom.error("不能重复添加！");
          } else {
            // 新增
            this.$ajax.post("ou", this.editData).then(res => {
              this.editVisible = false;
              this.ous.push(res.data);
            });
            commom.success("添加成功！");
            this.editVisible = false;
            // commom.error("不能重复添加！");
          }
        }
      });
    },
    editCancer: function() {
      this.editVisible = false;
    },
    // 删除
    del(index) {
      this.$confirm("此操作将永久删除该机构, 是否继续?", "提示", {
        type: "warning"
      })
        .then(() => {
          this.ous.splice(index, 1);
          commom.success("删除成功！");
        })
        .catch(() => {});
    },
    delMulti() {
      for (var i = this.selIds.length; i >= 0; i--) {
        var index = data.ou.indexOf(this.selIds[i]);
        if (index > -1) {
          this.ou.splice(index, 1);
        }
      }
      commom.success("批量删除成功！");
    }
  },
  created() {
    this.$ajax.get("ou").then(res => {
      this.ous = res.data;
    });
  }
};
</script>

<style scoped>

</style>
