import _ from "lodash";
import Vue from "vue";
import App from "./App.vue";
import "font-awesome/css/font-awesome.css";
require("./style/common.css");
import common from "./common.js";

import ElementUI from "element-ui";
import "../node_modules/element-ui/lib/theme-chalk/index.css";
Vue.use(ElementUI);

import router from "./route.js";
import store from "./store.js";
import filter from "./filter.js";

import Axios from "axios";
Vue.prototype.$ajax = Axios;
Axios.defaults.baseURL = "http://localhost:8888/api";
var loading = null;
Axios.interceptors.request.use(function(config) {
  loading = ElementUI.Loading.service({
    lock: true,
    text: "拼命加载中",
    spinner: "el-icon-loading",
    background: "rgba(0, 0, 0, 0.7)"
  });
  if (
    store != null &&
    store.state != null &&
    store.state.token != null &&
    store.state.token.access_token != null
  ) {
    config.headers.Authorization = "Bearer " + store.state.token.access_token;
  }
  return config;
});
Axios.interceptors.response.use(
  response => {
    loading.close();
    if (response.data.state != null && response.data.state == false) {
      common.error(response.data.msg);
    } else {
      return response;
    }
  },
  error => {
    loading.close();
    if (error.message != null) {
      common.error(error.message);
    } else if (
      error.response != null &&
      error.response.data != null &&
      error.response.data.Message != null
    ) {
      common.error(error.response.data.Message);
    }
    if (error.response != null && error.response.status == 401) {
      // 权限过期，处理
      router.push("login");
    }
    return Promise.reject(error);
  }
);

const app = new Vue({
  el: "#app",
  store,
  router,
  render: h => h(App)
});
