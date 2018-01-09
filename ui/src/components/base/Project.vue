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
            <el-button type="primary" @click="queryClick">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="add">新增</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <!--列表-->
      <el-table :data="user" highlight-current-row stripe border fit @selection-change="select">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column type="index" label="序号" width="80"></el-table-column>
        <el-table-column prop="name" label="姓名" width="100"></el-table-column>
        <el-table-column prop="department_id" label="部门" :formatter="filterDepartment" width="200"></el-table-column>
        <el-table-column prop="sex" :formatter="filterSex" label="性别" width="100" sortable></el-table-column>
        <el-table-column prop="age" label="年龄" width="100" sortable></el-table-column>
        <el-table-column prop="tel" label="电话" width="150"></el-table-column>
        <el-table-column prop="email" label="邮箱" width="200"></el-table-column>
        <el-table-column prop="address" label="地址"></el-table-column>
        <el-table-column label="操作" width="140">
          <template slot-scope="scope">
            <el-button size="small" @click="edit(scope.$index)">编辑</el-button>
            <el-button type="danger" size="small" @click="del(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <!--工具条-->
      <el-col :span="24" class="toolbar">
        <el-button type="danger" @click="delMulti" :disabled="this.selIds.length===0">批量删除</el-button>
        <el-pagination layout="prev, pager, next" :page-size="20" @current-change="pageIndexChange" :total="user.length" style="float:right;"></el-pagination>
      </el-col>
    </div>

    <!-- 新增、编辑界面 -->
    <div>
      <el-dialog :title="editTitle" v-model="editVisible" :close-on-click-modal="false">
        <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
          <el-form-item label="项目名称" prop="name">
            <el-input v-model="editData.name" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="项目别名" prop="nickName">
            <el-input v-model="editData.nickName" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="立项时间" prop="sex">
            <el-date-picker v-model="editData.creatTime" type="date" placeholder="选择日期"></el-date-picker>
          </el-form-item>
          <el-form-item label="结束时间" prop="age">
            <el-input v-model.number="editData.age" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="项目负责人" prop="tel">
            <el-input v-model="editData.tel" auto-complete="off"></el-input>
          </el-form-item>
          <el-form-item label="项目成员" prop="email">
            <el-input v-model="editData.email" auto-complete="off"></el-input>
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
        user:[],
        selIds: [], // 列表选中列id
        filters: {
					name: '',
          pageIndex : 1
				},
        // 新增
        editTitle:"新增部门",
        editVisible: false,
        editData:{
          department_id: null,
          name: '',
          sex: 0,
          age: 0,
          tel: '',
          email: '',
          address: ''
        },
        editRule: {
					name: [
            { required: true, message: '请输入人员名称', trigger: 'blur' },
            { min: 2, max: 5, message: '长度在 2 到 5 个字符', trigger: 'blur' }
          ],
          age:[ { type: 'number', message: '请输入正确的年龄格式', trigger: 'blur' } ],
          email: [
            { type: 'email', message: '请输入正确的邮箱格式', trigger: 'blur' }
          ]
				}
			}
		},
		methods: {
      select:function(selection) {
        this.selIds = selection;
      },
      queryClick() {
        this.user = data.user.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      pageIndexChange() {
        this.user = data.user.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      filterSex(row, column){
        var sexObj = { 0: "男", 1: "女" };
        return sexObj[row.sex];
      },
      filterDepartment(row, column){
        if(this.department[row.department_id]) {
          return this.department[row.department_id].name;
        }
      },
      // 新增
      add:function(){
        this.editTitle = "新增部门";
        this.editVisible = true;
        this.editData = {
          department_id: null,
          name: '',
          sex: 0,
          age: null,
          tel: '',
          email: '',
          address: ''
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
            if(this.editData.name.indexOf(this.user) == -1) {
              this.user[this.editData.index] = this.editData;
              commom.success("修改成功！");
              this.editVisible = false;
            } else {
              commom.error("不能重复添加！");
            }
          } else { // 新增
            if(this.editData.name.indexOf(this.user) == -1) {
              this.user.push(this.editData);
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
        this.editData = this.user[index];
        this.editData.index = index;

        this.editTitle = "编辑人员";
        this.editVisible = true;
      },
      // 删除
      del(index) {
        this.$confirm('此操作将永久删除该部门, 是否继续?', '提示', {
          type: 'warning'
        }).then(() => {
          this.user.splice(index,1);
          commom.success("删除成功！");
        }).catch(() => {
        });
      },
      delMulti() {
        for(var i=this.selIds.length;i>=0;i--) {
          var index = data.user.indexOf(this.selIds[i]);
          if(index > -1) {
            this.user.splice(index,1);
          }
        }
        commom.success("批量删除成功！");
      }
		},
		mounted() {
        this.user = data.user;
        this.department = data.department;
		}
	}
</script>

<style scoped>

</style>
