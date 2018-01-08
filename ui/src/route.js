import Vue from 'vue'
import Router from 'vue-router'
// import filter from './commom/filter.js'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/Login',
      component: require('./components/Login.vue'),
      hidden: true
    },
    {
      path: '/',
      name: '基础管理',
      alias: '/Default',
      component: resolve => require(["./components/Home.vue"], resolve),
      iconCls: 'el-icon-message',
      children: [
        { path: '/Company', name: '公司管理', component: resolve => require(['./components/base/Company.vue'], resolve) },
        { path: '/Department', name: '部门管理', component: resolve => require(['./components/base/Department.vue'], resolve) },
        { path: '/User', name: '人员管理', component: resolve => require(['./components/base/User.vue'], resolve) },
        { path: '/Project', name: '项目管理', component: resolve => require(['./components/base/Project.vue'], resolve) },
        { path: '/Default', name: '首页', component: resolve => require(['./components/Default.vue'], resolve), alias: '/', hidden: true },
      ]
    },
    {
      path: '*',
      component: resolve => require(["./components/404.vue"], resolve),
      hidden: true
    }
  ]
})
