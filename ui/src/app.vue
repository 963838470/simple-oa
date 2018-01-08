<template>
  <div class="hello">
    <p v-for="user in users" :key="user.Id">
      {{ user.Name }}:{{ user }}
    </p>
    <el-radio v-model="radio" label="1">备选项</el-radio>
    <el-radio v-model="radio" label="2">备选项</el-radio>
    <el-button>Let's do it</el-button>
  </div>
</template>
<script>
import topHeader from "./components/header.vue";
export default {
  data: function() {
    return {
      users: [],
      radio: 1
    };
  },
  created() {
    this.$ajax.get("user").then(res => {
      console.log(res.data);
      this.users = res.data;
    });
  },
  computed: {
    count() {
      return this.$store.state.count;
    },
    user() {
      return this.$store.getters.getuser(1);
    }
  },
  methods: {
    btnClick() {
      this.$store.commit("addCount", 10);
    }
  },
  components: {
    topHeader
  }
};
</script>
<style>
:fullscreen a {
  display: flex;
}
</style>


