<template>
  <div class="container">
    <div class="wrap">
      <p>
        <span class="title">SimpleOA系统</span>
      </p>
      <div class="main_content">
        <p class="no-margin">用户登录</p>
        <form>
          <p>
            <i class="fa fa-user-o"></i>
            <input type="text" class="login_input" placeholder="请输入用户名" v-model="loginName">
          </p>
          <p>
            <i class="fa fa-lock"></i>
            <input type="password" class="login_input" placeholder="请输入密码" v-model="password">
          </p>
          <label class="remember">
            <input type="checkbox" v-model="isRemember" />记住我的登录信息
          </label>
          <button style="submit" class="login-btn" @click="loginClick">登 录</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loginName: "",
      password: "",
      isRemember: false
    };
  },
  methods: {
    loginClick() {
      if (this.isRemember) {
        // 记住我的登录信息
        localStorage.setItem("loginName", this.loginName);
        localStorage.setItem("password", this.password);
        localStorage.setItem("isRemember", this.isRemember);

        this.$ajax
          .post("../token", {
            grant_type: "password",
            username: this.loginName,
            password: this.password
          })
          .then(res => {
            debugger;
          });
      }
    }
  },
  created() {
    this.loginName = localStorage.getItem("loginName");
    this.password = localStorage.getItem("password");
    this.isRemember = localStorage.getItem("isRemember");
  }
};
</script>

<style scoped>
.container {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: url("../image/bg.jpg");
}

.wrap {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  width: 550px;
  height: 450px;
  margin: auto;
  font-family: "微软雅黑";
  font-size: 16px;
  color: white;
}

.title {
  line-height: 40px;
  font-size: 30px;
  font-weight: 500;
  margin: 20px 10px 10px;
}

.main_content {
  width: 100%;
  height: 380px;
  background: url("../image/main_bg.png") repeat;
  border-radius: 8px;
  padding: 20px;
}

.login_input {
  border: 1px solid #3872f6;
  border-radius: 3px;
  line-height: 40px;
  padding: 2px 5px 2px 30px;
  background: none;
  width: 100%;
  margin: 0 auto;
  color: white;
}

.login-btn {
  display: block;
  background: #3872f6;
  color: #fff;
  font-size: 15px;
  line-height: 50px;
  border-radius: 3px;
  border: none;
  width: 100%;
  margin: 0 auto;
}

p {
  line-height: 60px;
  margin-bottom: 20px;
  position: relative;
}

.fa {
  position: absolute;
  bottom: 20px;
  left: 10px;
  font-size: 18px;
  color: #3872f6;
}

.remember {
  float: left;
  margin-bottom: 20px;
}
</style>
