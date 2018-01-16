import _ from 'lodash';
import Vue from 'vue';
import App from './app.vue';
require("./style/common.css")
import common from './common/common.js'

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
    return response;
}
// , (error) => {
//     loading.close();
//     common.error("出错了");
//     return Promise.reject(error);
// }
)

import router from './route.js'
import store from './store.js'
import filter from './filter.js'

const app = new Vue({
    el: '#app',
    store,
    router,
    render: h => h(App)
});