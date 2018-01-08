import _ from 'lodash';
import Icon from './icon.png';
import Vue from 'vue';
import App from './app.vue';
require("./style/style.css")

import ElementUI from 'element-ui'
import '../node_modules/element-ui/lib/theme-chalk/index.css'
Vue.use(ElementUI)

import Vuex from 'vuex'
Vue.use(Vuex)
const store = new Vuex.Store({
    state: {
        count: 0,
        users: [
            { id: 1, name: '衣衣' },
            { id: 2, name: '二哈' },
            { id: 3, name: '张三' },
            { id: 4, name: '李四' },
            { id: 5, name: '王五' },
        ]
    },
    getters: {
        getuser: state => (id) => {
            return state.users.filter(o => o.id == id)
        }
    },
    mutations: {
        addCount(state, n) {
            state.count += n;
        }
    }
})
store.commit('addCount', 1)
store.state.count += 1;
// computed: {
//     count() {
//       return this.$store.state.count;
//     },
//     user() {
//       return this.$store.getters.getuser(1);
//     }
//   },
//   methods: {
//     btnClick() {
//       this.$store.commit("addCount", 10);
//     }
//   },

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

const app = new Vue({
    el: '#app',
    store,
    render: h => h(App)
});

if (module.hot) {

}