<template>
  <section>
    <!-- 列表页面 -->
    <div>
      <!--工具条-->
      <el-col :span="24" class="toolbar">
        <el-form :inline="true" :model="filters">
          <el-form-item>
            <el-input v-model="filters.name" placeholder="姓名"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="initUser">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="add">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <!--列表-->
      <el-table :data="users" highlight-current-row stripe border fit ref="table">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column label="操作" width="160">
          <template slot-scope="scope">
            <el-button size="mini" @click="edit(scope.row)">编辑</el-button>
            <el-button size="mini" type="danger" @click="del(scope.row)">删除</el-button>
          </template>
        </el-table-column>
        <el-table-column type="index" label="序号" width="80"></el-table-column>
        <el-table-column prop="name" label="姓名" width="100"></el-table-column>
        <el-table-column prop="email" label="邮箱" width="200"></el-table-column>
        <el-table-column prop="phone" label="电话" width="150"></el-table-column>
        <el-table-column prop="address" label="住址" width="100"></el-table-column>
        <el-table-column prop="remark" label="备注"></el-table-column>
      </el-table>
      <!--工具条-->
      <el-col :span="24" class="toolbar">
        <el-button type="danger" @click="delMulti" :disabled="this.$refs.table != null && this.$refs.table.selection.length<=0">批量删除</el-button>
        <el-pagination layout="total, prev, pager, next" background :page-size="filters.pageSize" @current-change="pageIndexChange" :total="totalCount" style="float:right;"></el-pagination>
      </el-col>
    </div>

    <!-- 新增、编辑界面 -->
    <div>
      <el-dialog :title="editTitle" :visible.sync="editVisible" :close-on-click-modal="false">
        <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
          <el-form-item label="机构">
            <el-cascader v-model="editOuPath" placeholder="若无所属机构则不填" :options="ouTree" style="width:100%;" change-on-select clearable :props="{value:'id',label:'name'}"></el-cascader>
          </el-form-item>
          <el-form-item label="名称" prop="name">
            <el-input v-model="editData.name" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="登陆名" prop="loginName">
            <el-input v-model="editData.loginName" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="密码" prop="password">
            <el-input v-model.number="editData.password" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="邮箱" prop="email">
            <el-input v-model="editData.email" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="电话" prop="phone">
            <el-input v-model="editData.phone" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="地址" prop="address">
            <el-input v-model="editData.address" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="备注" prop="remark">
            <el-input v-model="editData.remark" auto-complete="off"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="addCancer">取消</el-button>
          <el-button type="primary" @click="addConfirm">提交</el-button>
        </div>
      </el-dialog>
    </div>
  </section>
</template>

<script>
import common from "../../common.js";

export default {
  props: ["checkedOu", "ouTree"],
  data: function() {
    return {
      // 列表
      users: [],
      filters: {
        ouid: 0,
        name: "",
        pageIndex: 1,
        pageSize: 10
      },
      totalCount: 0,
      // 新增
      editTitle: "",
      editVisible: false,
      editData: {},
      editRule: {
        name: [
          { required: true, message: "请输入人员名称", trigger: "blur" },
          { min: 2, max: 5, message: "长度在 2 到 5 个字符", trigger: "blur" }
        ],
        age: [
          { type: "number", message: "请输入正确的年龄格式", trigger: "blur" }
        ],
        email: [
          { type: "email", message: "请输入正确的邮箱格式", trigger: "blur" }
        ]
      },
      editOuPath: []
    };
  },
  methods: {
    pageIndexChange(pageIndex) {
      this.filters.pageIndex = pageIndex;
      this.initUser();
    },
    // 新增
    add: function() {
      this.editTitle = "新增人员";
      this.editVisible = true;
      this.editData = {};
    },
    // 编辑
    edit(row) {
      this.editData = row;
      this.editTitle = "编辑人员";
      this.editVisible = true;
    },
    addConfirm() {
      var isValid = false;
      this.$refs["editData"].validate(function(valid) {
        // 表单校验
        if (valid) {
          isValid = true;
        }
      });
      if (isValid) {
        if (this.editData.id != null) {
          // 编辑
          this.$ajax.put("user", this.editData).then(res => {
            this.initUser();
            this.editVisible = false;
            common.success("编辑成功！");
          });
        } else {
          // 新增
          this.$ajax.post("user", this.editData).then(res => {
            if (this.editOuPath.length > 0) {
              this.$ajax
                .post("ouuser", {
                  ouId: this.editOuPath[this.editOuPath.length - 1],
                  userId: res.data.id
                })
                .then(res => {
                  this.initUser();
                  this.editVisible = false;
                  common.success("新增成功！");
                });
            }
          });
        }
      }
    },
    addCancer: function() {
      this.editVisible = false;
    },
    // 删除
    del(row) {
      this.$confirm("此操作将永久删除该人员, 是否继续?", "提示", {
        type: "warning"
      }).then(() => {
        this.$ajax.delete("user?ids=" + row.id).then(res => {
          this.initUser();
          common.success("删除成功！");
        });
      });
    },
    delMulti() {
      this.$confirm("此操作将永久删除该人员, 是否继续?", "提示", {
        type: "warning"
      }).then(() => {
        var selection = this.$refs.table.selection;
        var ids = [];
        for (var i = 0; i < selection.length; i++) {
          ids.push(selection[i].id);
        }
        this.$ajax.delete("user?ids=" + ids.join(",")).then(res => {
          this.initUser();
          common.success("批量删除成功！");
        });
      });
    },
    initUser() {
      console.log(this.checkedOu)
      console.log({
            name: this.filters.name,
            pageIndex: this.filters.pageIndex,
            pageSize: this.filters.pageSize,
            ouId: this.checkedOu.id
          });
      this.$ajax
        .get("user", {
          params: {
            name: this.filters.name,
            pageIndex: this.filters.pageIndex,
            pageSize: this.filters.pageSize,
            ouId: this.checkedOu.id
          }
        })
        .then(res => {
          this.users = res.data.data;
          this.totalCount = res.data.count;
        });
    }
  },
  watch: {
    ouId() {
      this.initUser();
    },
    checkedOu() {
      this.editOuPath = common.convertIntArray(
        this.checkedOu.path + "," + this.checkedOu.id
      );
      this.initUser();
      console.log(this.checkedOu);
    }
  },
  mounted() {
    this.initUser();
  }
};
</script>

<style scoped>

</style>
