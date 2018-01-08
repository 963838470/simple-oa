<template>
  <section>
    <!-- 列表页面 -->
    <div>
      <!--工具条-->
      <el-col :span="24" class="toolbar">
        <el-form :inline="true" :model="filters">
          <el-form-item>
            <el-input v-model="filters.name" placeholder="部门名称"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="queryClick">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="add">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <!--列表-->
      <el-table :data="department" highlight-current-row stripe border fit @selection-change="select">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column type="index" label="序号" width="80"></el-table-column>
        <el-table-column prop="name" label="部门名称" width="200"></el-table-column>
        <el-table-column label="操作" width="140">
          <template scope="scope">
            <el-button size="small" @click="edit(scope.$index)">编辑</el-button>
            <el-button type="danger" size="small" @click="del(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <!--工具条-->
      <el-col :span="24" class="toolbar">
        <el-button type="danger" @click="delMulti" :disabled="this.selIds.length===0">批量删除</el-button>
        <el-pagination layout="prev, pager, next" :page-size="20" @current-change="pageIndexChange" :total="department.length" style="float:right;"></el-pagination>
      </el-col>
    </div>

    <!-- 新增、编辑界面 -->
    <div>
      <el-dialog :title="editTitle" v-model="editVisible" :close-on-click-modal="false">
        <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
          <el-form-item label="名称" prop="name">
            <el-input v-model="editData.name" auto-complete="off"></el-input>
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
  import data from '../../commom/data'
  import commom from '../../commom/commom'
	export default {
		data() {
			return {
        // 列表
        department:[],
        selIds: [], // 列表选中列id
        filters: {
					name: '',
          pageIndex : 1
				},
        // 新增
        editTitle:"新增部门",
        editVisible: false,
        editData:{
          name:"",
        },
        editRule: {
					name: [
						{ required: true, message: '请输入部门名称', trigger: 'blur' }
					]
				}
			}
		},
		methods: {
      select:function(selection) {
        this.selIds = selection;
      },
      queryClick() {
        this.department = data.department.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      pageIndexChange() {
        this.department = data.department.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      // 新增
      add:function(){
        this.editTitle = "新增部门";
        this.editVisible = true;
        this.editData = {
          id: commom.getRandom(),
          name:""
        };
      },
      addConfirm:function(){
        var isValid = false;
        this.$refs['editData'].validate(function(valid) { // 表单校验
          if (valid) {
            isValid = true;
          }
        });
        if(isValid) {
          if(this.editData.index != null && this.editData.index >= 0) { // 编辑
            if(this.editData.name.indexOf(this.department) == -1) {
              this.department[this.editData.index] = this.editData;
              commom.success("修改成功！");
              this.editVisible = false;
            } else {
              commom.error("不能重复添加！");
            }
          } else { // 新增
            if(this.editData.name.indexOf(this.department) == -1) {
              this.department.push(this.editData);
              commom.success("添加成功！");
              this.editVisible = false;
            } else {
              commom.error("不能重复添加！");
            }
          }
        }
      },
      addCancer:function(){
        this.editVisible = false;
      },
      // 编辑
      edit(index) {
        this.editData = this.department[index];
        this.editData.index = index;

        this.editTitle = "编辑部门";
        this.editVisible = true;
      },
      // 删除
      del(index) {
        this.$confirm('此操作将永久删除该部门, 是否继续?', '提示', {
          type: 'warning'
        }).then(() => {
          this.department.splice(index,1);
          commom.success("删除成功！");
        }).catch(() => {
        });
      },
      delMulti() {
        for(var i=this.selIds.length;i>=0;i--) {
          var index = data.department.indexOf(this.selIds[i]);
          if(index > -1) {
            this.department.splice(index,1);
          }
        }
        commom.success("批量删除成功！");
      }
		},
		mounted() {
        this.department = data.department;
		}
	}
</script>

<style scoped>

</style>
