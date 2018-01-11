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
  </div>
</template>

<script>
export default {
  data() {
    return {
      filterText: "",
      ous: [],
      checkedOu: {}
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
                    console.info("点击了节点" + data.id + "的添加按钮");
                    console.log(data);
                    store.append(
                      { id: self.baseId++, label: "testtest", children: [] },
                      data
                    );
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
                    console.info("点击了节点" + data.id + "的添加按钮");
                    console.log(data);
                    store.append(
                      { id: self.baseId++, label: "testtest", children: [] },
                      data
                    );
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
                    console.info("点击了节点" + data.id + "的添加按钮");
                    console.log(data);
                    store.append(
                      { id: self.baseId++, label: "testtest", children: [] },
                      data
                    );
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
    this.$ajax.get("ou").then(res => {
      this.ous = res.data;
      this.checkedOu = res.data[0];
    });
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
