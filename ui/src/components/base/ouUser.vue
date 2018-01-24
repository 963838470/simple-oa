<template>
  <div>
    <el-row>
      <el-col :span="6">
        <el-col class="toolbar">
          <el-input placeholder="输入机构名称进行过滤" v-model="filterText"></el-input>
        </el-col>
        <el-col>
          <el-tree class="filter-tree" :data="ouTree" ref="ouTree" highlight-current @node-click="handleNodeClick" :render-content="renderContent" @contextmenu.prevent="rightClick" :filter-node-method="filterNode" change-on-select :props="{label:'name'}">
          </el-tree>
        </el-col>
      </el-col>
      <el-col :span="18">
        <user :ou-id="checkedOu.id" :edit-ou-path="editOuPath" :ou-tree="ouTree"></user>
      </el-col>
    </el-row>

    <!-- 新增、编辑机构界面 -->
    <el-dialog :title="editTitle" :visible.sync="editVisible" :close-on-click-modal="false">
      <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
        <el-form-item label="上级机构">
          <el-cascader v-model="editOuPath" placeholder="若无上级机构则不填" :options="ouTree" style="width:100%;" change-on-select clearable :props="{value:'id',label:'name'}"></el-cascader>
        </el-form-item>
        <el-form-item label="机构名称" prop="name">
          <el-input v-model="editData.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="机构地址">
          <el-input v-model="editData.address" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input type="textarea" autosize v-model="editData.description" auto-complete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editCancer">取消</el-button>
        <el-button type="primary" @click="editConfirm">提交</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import common from "../../common.js";
import user from "./user.vue";
export default {
  data() {
    return {
      filterText: "",
      ouTree: [],
      ous: [],
      checkedOu: {},
      // 编辑
      editTitle: "",
      editVisible: false,
      editData: {},
      editOuPath: [],
      editRule: {
        name: [{ required: true, message: "请输入机构名称", trigger: "blur" }]
      }
    };
  },
  watch: {
    filterText(val) {
      this.$refs.ouTree.filter(val);
    }
  },
  methods: {
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    handleNodeClick(data) {
      this.checkedOu = data;
    },
    initOu() {
      this.$ajax.get("ouTree").then(res => {
        this.ouTree = res.data;
        this.checkedOu = res.data[0];
        this.$ajax.get("ou").then(res => {
          this.ous = res.data;
        });
      });
    },
    rightClick(data) {},
    editConfirm() {
      var self = this;
      this.$refs["editData"].validate(valid => {
        if (valid) {
          this.editData.pid =
            this.editOuPath.length > 0
              ? this.editOuPath[this.editOuPath.length - 1]
              : 0;
          this.editData.path = this.editOuPath.join(",");
          if (this.editData.id != null) {
            // 编辑
            this.$ajax.put("ou", this.editData).then(res => {
              this.editVisible = false;
              this.initOu();
              common.success("修改成功！");
            });
          } else {
            // 新增
            this.$ajax.post("ou", this.editData).then(res => {
              this.editVisible = false;
              this.initOu();
              common.success("添加成功！");
            });
          }
        } else {
          common.success("表单校验不成功！");
        }
      });
    },
    editCancer: function() {
      this.editVisible = false;
    },
    renderContent: function(createElement, { node, data, store }) {
      var self = this;
      return createElement(
        "span",
        {
          attrs: {
            style: "width: 100%; line-height:26px;"
          }
        },
        [
          createElement(
            "span",
            {
              attrs: {
                style: "float: left;"
              }
            },
            node.label
          ),
          createElement(
            "span",
            {
              attrs: {
                class: "ou_icons"
              }
            },
            [
              createElement("i", {
                attrs: {
                  class: "el-icon-plus",
                  title: "新增"
                },
                on: {
                  click: function() {
                    self.editTitle = "新增机构";
                    self.editVisible = true;
                    self.editData = {};
                    self.editOuPath = common.convertIntArray(
                      data.path + "," + data.id
                    );
                    return;
                  }
                }
              }),
              createElement("i", {
                attrs: {
                  class: "el-icon-edit",
                  title: "编辑"
                },
                on: {
                  click: function() {
                    self.editTitle = "编辑机构";
                    self.editVisible = true;
                    self.editData = data;
                    self.editOuPath = common.convertIntArray(data.path);
                    return;
                  }
                }
              }),
              createElement("i", {
                attrs: {
                  class: "el-icon-delete",
                  title: "删除"
                },
                on: {
                  click: function() {
                    self
                      .$confirm("此操作将永久删除该机构, 是否继续?", "提示", {
                        type: "warning"
                      })
                      .then(() => {
                        self.$ajax.delete("ou?ids=" + data.id).then(res => {
                          self.initOu();
                          common.success(res.data);
                        });
                      })
                      .catch(() => {});
                  }
                }
              })
            ]
          )
        ]
      );
    }
  },
  created() {
    this.initOu();
  },
  components: {
    user
  }
};
</script>

<style>
.ou_icons {
  float: right;
}

.ou_icons i {
  margin-right: 10px;
}
</style>
