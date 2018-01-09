<template>
	<section>
    <!--工具条-->
    <el-col :span="24" class="toolbar">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input v-model="filters.name" placeholder="机构名称"></el-input>
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
		<el-table :data="ou" highlight-current-row v-loading="listLoading" stripe border fit @selection-change="select">
			<el-table-column type="selection" width="55"></el-table-column>
			<el-table-column type="index" label="序号" width="80"></el-table-column>
			<el-table-column prop="name" label="机构名称" width="200"></el-table-column>
			<el-table-column prop="simpleName" label="机构简称" width="150"></el-table-column>
			<el-table-column prop="leader" label="负责人" width="120"></el-table-column>
			<el-table-column prop="summary" label="机构简介"></el-table-column>
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
      <el-pagination layout="prev, pager, next" :page-size="20" @current-change="pageIndexChange" :total="ou.length" style="float:right;"></el-pagination>
    </el-col>

		<!-- 新增、编辑界面 -->
    <el-dialog :title="editTitle" v-model="editVisible" :close-on-click-modal="false">
      <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
        <el-form-item label="机构名称" prop="name">
          <el-input v-model="editData.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="机构简称" prop="simpleName">
          <el-input v-model="editData.simpleName" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="负责人" prop="leader">
          <el-input v-model="editData.leader" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="机构简介" prop="summary">
          <el-input v-model="editData.summary" auto-complete="off" type="textarea" autosize></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="addCancer">取消</el-button>
        <el-button type="primary" @click="addConfirm">提交</el-button>
      </div>
    </el-dialog>
	</section>
</template>

<script>
  import data from '../../commom/data.js'
  import commom from '../../commom/commom'

	export default {
		data() {
			return {
        ou:[],
				filters: {
					name: '',
          pageIndex : 1
				},
        selIds: [], // 列表选中列id
				users: [],
				total: 0,
				page: 1,
				listLoading: false,
				sels: [],//列表选中列

				// 新增
        editTitle:"",
        editVisible: false,
        editData:{
          name: '',
          simpleName: '',
          leader: '',
          summary: '',
        },
        editRule: {
					name: [
            { required: true, message: '请输入机构名称', trigger: ['blur','change'] },
          ],
          leader:[
            { required: true, message: '请输入负责人', trigger: ['blur','change'] }
          ],
				}

			}
		},
		methods: {
      select:function(selection) {
        this.selIds = selection;
      },
      queryClick() {
        this.ou = data.ou.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      pageIndexChange() {
        this.ou = data.ou.filter((data)=> {
          return data.name.indexOf(this.filters.name) > -1;
        });
      },
      // 新增
      add:function(){
        this.editTitle = "新增机构";
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
            if(this.editData.name.indexOf(this.ou) == -1) {
              this.ou[this.editData.index] = this.editData;
              commom.success("修改成功！");
              this.editVisible = false;
            } else {
              commom.error("不能重复添加！");
            }
          } else { // 新增
            if(this.editData.name.indexOf(this.ou) == -1) {
              this.ou.push(this.editData);
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
        this.editData = this.ou[index];
        this.editData.index = index;

        this.editTitle = "编辑机构";
        this.editVisible = true;
      },
      // 删除
      del(index) {
        this.$confirm('此操作将永久删除该机构, 是否继续?', '提示', {
          type: 'warning'
        }).then(() => {
          this.ou.splice(index,1);
          commom.success("删除成功！");
        }).catch(() => {
        });
      },
      delMulti() {
        for(var i=this.selIds.length;i>=0;i--) {
          var index = data.ou.indexOf(this.selIds[i]);
          if(index > -1) {
            this.ou.splice(index,1);
          }
        }
        commom.success("批量删除成功！");
      }
		},
		mounted() {
      this.ou = data.ou;
      console.log(data.ou);
		}
	}

</script>

<style scoped>

</style>
