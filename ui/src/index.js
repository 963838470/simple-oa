import _ from 'lodash';
import Vue from 'vue';
import App from './app.vue';
require("./style/common.css")

import ElementUI from 'element-ui'
// import 'element-ui/lib/theme-default/index.css'
import '../node_modules/element-ui/lib/theme-chalk/index.css'
Vue.use(ElementUI)

import Axios from 'axios'
Vue.prototype.$ajax = Axios;
Axios.defaults.baseURL = 'http://localhost:8888/api';
//Axios.defaults.headers.common['Content-Type'] = 'application/json;charset=UTF-8'
var loading = null;
Axios.interceptors.request.use(function (config) {
    // Mint.Indicator.open({
    //     text: '加载中...',
    //     spinnerType: 'fading-circle'
    // });
    // config.headers['X-Requested-With'] = 'XMLHttpRequest'
    loading = ElementUI.Loading.service({
        lock: true,
        text: '拼命加载中',
        spinner: 'el-icon-loading',
        background: 'rgba(0, 0, 0, 0.7)'
    });
    return config;
})
Axios.interceptors.response.use(function (config) {
    loading.close();
    return config;
})

import router from './route.js'
import store from './store.js'
import filter from './filter.js'

const app = new Vue({
    el: '#app',
    store,
    router,
    render: h => h(App)
});

if (module.hot) {

}