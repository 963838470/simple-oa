import Vue from "vue"
import Router from "vue-router"
import Login from "./components/Login.vue"

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: "/login",
      component: Login,
      hidden: true
    },
    {
      path: "/",
      name: "基础管理",
      alias: "/default",
      component: resolve => require(["./components/HomePage.vue"], resolve),
      iconCls: "el-icon-message",
      children: [
        { path: "/ouuser", name: "机构人员", component: resolve => require(["./components/base/OuUser.vue"], resolve) },
        { path: "/project", name: "项目管理", component: resolve => require(["./components/base/Project.vue"], resolve) },
        { path: "/default", name: "首页", component: resolve => require(["./components/base/Default.vue"], resolve), alias: "/", hidden: true }
      ]
    },
    {
      path: "*",
      component: resolve => require(["./components/404.vue"], resolve),
      hidden: true
    }
  ]
})
