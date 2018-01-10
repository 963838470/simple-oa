<template>
  <div>
    <el-row>
      <el-col :span="6">
        <el-input placeholder="输入关键字进行过滤" v-model="filterText"></el-input>
        <el-tree class="filter-tree" :data="ous" :props="defaultProps" default-expand-all :filter-node-method="filterNode" ref="tree2">
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
      defaultProps: {
        children: "children",
        label: "label"
      }
    };
  },
  watch: {
    filterText(val) {
      this.$refs.tree2.filter(val);
    }
  },
  methods: {
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
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
