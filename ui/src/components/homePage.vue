<template>
  <div>
    <header>
      <my-head></my-head>
    </header>
    <section>
      <div class="menu">
        <my-menu></my-menu>
      </div>
      <!-- 面包屑 -->
      <div class="main-container">
        <el-breadcrumb separator="/" class="breadcrumb">
          <el-breadcrumb-item v-for="item in $route.matched" :key="item.path">
            {{ item.name }}
          </el-breadcrumb-item>
        </el-breadcrumb>
        <router-view class="main"></router-view>
      </div>
    </section>
  </div>
</template>

<script>
import myHead from "./header.vue";
import myMenu from "./menu.vue";

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

<style scoped>
.main {
  margin: 0 15px;
}

.breadcrumb {
  line-height: 20px;
  margin: 10px 15px 0;
}

.menu {
  position: absolute;
  top: 60px;
  bottom: 0;
  left: 0;
  width: 240px;
}

.main-container {
  position: absolute;
  top: 60px;
  bottom: 0;
  left: 240px;
  right: 0;
  overflow: auto;
}
</style>

