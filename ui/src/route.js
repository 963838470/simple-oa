import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/login',
      component: require('./components/login.vue'),
      hidden: true
    },
    {
      path: '/',
      name: '基础管理',
      alias: '/default',
      component: resolve => require(["./components/homePage.vue"], resolve),
      iconCls: 'el-icon-message',
      children: [
        { path: '/ouuser', name: '机构人员', component: resolve => require(['./components/base/ouUser.vue'], resolve) },
        { path: '/company', name: '公司管理', component: resolve => require(['./components/base/company.vue'], resolve) },
        { path: '/department', name: '部门管理', component: resolve => require(['./components/base/department.vue'], resolve) },
        { path: '/user', name: '人员管理', component: resolve => require(['./components/base/user.vue'], resolve) },
        { path: '/project', name: '项目管理', component: resolve => require(['./components/base/project.vue'], resolve) },
        { path: '/default', name: '首页', component: resolve => require(['./components/base/default.vue'], resolve), alias: '/', hidden: true },
      ]
    },
    {
      path: '*',
      component: resolve => require(["./components/404.vue"], resolve),
      hidden: true
    }
  ]
})
