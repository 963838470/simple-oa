<template>
  <div class="header">
    <h1>
      <router-link to="/">Simple_OA系统</router-link>
    </h1>
    <h3>
      <span>欢迎您：</span>
      <span>{{ $store.state.ou[0].name }}</span>
      <span>{{ $store.state.user.name }}</span>
    </h3>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.header {
  background: #20a0ff;
  line-height: 60px;
  height: 60px;
}

h1 {
  float: left;
  margin-left: 50px;
}

h3 {
  float: right;
  margin-right: 50px;
  font-weight: normal;
  font-size: 16px;
  color: white;
}

a {
  text-decoration: none;
  color: white;
}
</style>
