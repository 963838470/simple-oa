<template>
  <div>
    <header>
      <BaseHead></BaseHead>
    </header>
    <section>
      <div class="menu">
        <BaseMenu></BaseMenu>
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
import BaseHead from "./BaseHeader.vue";
import BaseMenu from "./BaseMenu.vue";

export default {
  data() {
    return {};
  },
  components: {
    BaseHead,
    BaseMenu
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

