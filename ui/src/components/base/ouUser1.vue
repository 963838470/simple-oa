<template>
  <div>
    <el-tree :data="ous" ref="ouTree"></el-tree>
  </div>
</template>

<script>
//import common from "../../common/common.js";
export default {
  data() {
    return {
      filterText: "",
      ous: [{ name: 12, label: 13 }],
      checkedOu: {}
    };
  },
  methods: {
    handleNodeClick(data) {
      console.log(data);
      return;
    },
    initOu() {
      this.$ajax.get("ou").then(res => {
        this.ous = res.data;
        this.checkedOu = res.data[0];
      });
    }
  },
  created() {
    //this.initOu();
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
