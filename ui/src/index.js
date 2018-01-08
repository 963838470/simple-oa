import _ from 'lodash';
import Icon from './icon.png';
import Vue from 'vue';
import Vuex from 'vuex'

import App from './views/app.vue';
import { debug } from 'util';
require("./style.css")

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
store.commit('addCount',1)
store.state.count += 1;
console.log(store.state.count)

const app = new Vue({
    el: '#app',
    store,
    render: h => h(App)
});

if (module.hot) {

}