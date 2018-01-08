<template>
  <div>
    <header>
      <my-head></my-head>
    </header>
    <section>
      <el-col :span="3">
        <my-menu></my-menu>
      </el-col>
      <!-- 面包屑 -->
      <el-col :span="21">
        <el-breadcrumb separator="/" class="breadcrumb">
          <el-breadcrumb-item v-for="item in $route.matched" :key="item.path">
            {{ item.name }}
          </el-breadcrumb-item>
        </el-breadcrumb>
        <router-view class="main"></router-view>
      </el-col>
    </section>
  </div>
</template>

<script>
import myHead from "./header.vue";
import myMenu from "./Menu.vue";

export default {
  data() {
    return {};
  },
  components: {
    myHead,
    myMenu
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.main {
  margin: 0 15px;
}

.breadcrumb {
  line-height: 20px;
  margin: 10px 15px 0;
}
</style>

