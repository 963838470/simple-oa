<template>
  <div class="hello">
    <top-header></top-header>
    这是{{ page }}页面{{ count }}
    <button type="button" @click="btnClick">父组件点击增加</button>
    <p>
      {{ user }}
    </p>
  </div>
</template>
<script>
import topHeader from "../components/header.vue";
import { mapState } from "vuex";
export default {
  data: function() {
    return {
      page: "父组件"
    };
  },
  created() {
    class Animal {
      constructor() {
        this.type = "动物";
      }
      says(say) {
        console.log(this.type + " says " + say);
      }
    }
    let animal = new Animal();
    animal.says("hello"); //animal says hello

    class Cat extends Animal {
      constructor() {
        super();
        this.type = "猫";
      }
    }
    let cat = new Cat();
    cat.says("hello");
  },
  computed: {
    count() {
      return this.$store.state.count;
    },
    user() {
      return this.$store.getters.getuser(3);
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


