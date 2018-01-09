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
Axios.interceptors.request.use(function (config) {
    // Mint.Indicator.open({
    //     text: '加载中...',
    //     spinnerType: 'fading-circle'
    // });
    // config.headers['X-Requested-With'] = 'XMLHttpRequest'
    return config;
})
Axios.interceptors.response.use(function (config) {
    // Mint.Indicator.close();
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