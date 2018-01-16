<template>
  <div>
    <el-row>
      <el-col :span="6">
        <el-input placeholder="输入机构名称进行过滤" v-model="filterText"></el-input>
        <el-tree class="filter-tree" :data="ous" ref="ouTree" highlight-current @node-click="handleNodeClick" :render-content="renderContent" @contextmenu.prevent="rightClick" :filter-node-method="filterNode">
        </el-tree>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light"></div>
      </el-col>
    </el-row>

    <!-- 新增、编辑机构界面 -->
    <el-dialog :title="editTitle" :visible.sync="editVisible" :close-on-click-modal="false">
      <el-form :model="editData" label-width="80px" :rules="editRule" ref="editData">
        <el-form-item label="机构名称" prop="name">
          <el-input v-model="editData.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="机构地址" prop="address">
          <el-input v-model="editData.address" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="描述" prop="description">
          <el-input v-model="editData.description" auto-complete="off"></el-input>
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
import common from "../../common/common.js";
export default {
  data() {
    return {
      filterText: "",
      ous: [],
      checkedOu: {},
      // 编辑
      editTitle: "",
      editVisible: false,
      editData: {
        name: null
      },
      editRule: {
        name: [{ required: true, message: "请输入机构名称", trigger: ["blur"] }],
        address: [{ required: false, message: "请输入机构名称", trigger: ["blur"] }],
        description: [
          { required: false, message: "请输入机构名称", trigger: ["blur"] }
        ]
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
      console.log(data);
    },
    initOu() {
      this.$ajax.get("ou").then(res => {
        this.ous = res.data;
        this.checkedOu = res.data[0];
      });
    },
    rightClick(data) {
      console.log(data);
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
                        self.$ajax.delete("ou/" + data.id).then(res => {
                          self.initOu();
                          store.remove(data);
                          common.success("删除成功！");
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
    },
    editConfirm() {
      var self = this;
      this.$refs["editData"].validate(valid => {
        if (valid) {
          if (this.editData.id != null) {
            // 编辑
            this.$ajax.put("ou", this.editData).then(res => {
              this.editVisible = false;
              self.initOu();
              common.success("修改成功！");
            });
          } else {
            // 新增
            this.$ajax.post("ou", this.editData).then(res => {
              this.editVisible = false;
              self.initOu();
              common.success("添加成功！");
            });
          }
        } else {
          common.success("失败");
        }
      });
    },
    editCancer: function() {
      this.editVisible = false;
    }
  },
  created() {
    this.initOu();
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
