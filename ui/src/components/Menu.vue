<template>
  <div>
      <el-menu :default-active="$route.path" class="el-menu-vertical-demo" :router="true" unique-opened>
        <el-submenu :index="index+''" :key="index+''" v-for="(route,index) in $router.options.routes" v-if="!route.hidden">
          <template slot="title">{{ route.name }}</template>
            <el-menu-item v-for="r in route.children" v-if="!r.hidden" :index="r.path" :key="r.path">{{ r.name }}</el-menu-item>
        </el-submenu>
      </el-menu>
  </div>
</template>

<script>
export default {
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>

