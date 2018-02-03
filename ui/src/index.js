import _ from 'lodash';
import Vue from 'vue';
import App from './App.vue';
import 'font-awesome/css/font-awesome.css'
require("./style/common.css")
import common from './common.js'

import ElementUI from 'element-ui'
import '../node_modules/element-ui/lib/theme-chalk/index.css'
Vue.use(ElementUI)

import Axios from 'axios'
Vue.prototype.$ajax = Axios;
Axios.defaults.baseURL = 'http://localhost:8888/api';
var loading = null;
Axios.interceptors.request.use(function (config) {
    loading = ElementUI.Loading.service({
        lock: true,
        text: '拼命加载中',
        spinner: 'el-icon-loading',
        background: 'rgba(0, 0, 0, 0.7)'
    });
    return config;
})
Axios.interceptors.response.use((response) => {
    loading.close();
    if (response.data.state != null && response.data.state == false) {
        common.error(response.data.msg);
    } else {
        return response;
    }
}, (error) => {
    loading.close();
    common.error("请求异常");
    return Promise.reject(error);
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