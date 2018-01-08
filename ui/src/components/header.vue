<template>
  <div>
    自组件count:{{count}}
    <button type="button" @click="btnClick">子组件点击增加</button>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  data: function() {
    return {};
  },
  computed: {
    count() {
      return this.$store.state.count;
    }
  },
  methods: {
    btnClick() {
      this.$store.commit("addCount", 1);
    }
  }
};
</script>

<style scoped>

</style>
