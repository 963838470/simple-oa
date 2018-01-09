<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  created() {
    this.$ajax.get("user").then(res => {
      var users = res.data;
      if (users.length > 0) {
        this.$store.commit("setUser", users[0]);
      }
    });
    this.$ajax.get("ou").then(res => {
      var ou = res.data;
      if (ou.length > 0) {
        this.$store.commit("setOu", ou[0]);
      }
    });
  }
};
</script>

<style>

</style>
