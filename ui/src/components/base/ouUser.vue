<template>
  <div>
    <el-row>
      <el-col :span="6">
        <el-input placeholder="输入关键字进行过滤" v-model="filterText"></el-input>
        <el-tree class="filter-tree" :data="ous" ref="ouTree" highlight-current @node-click="handleNodeClick" :filter-node-method="filterNode">
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

<style scoped>

</style>
