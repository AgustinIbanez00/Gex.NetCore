<template>
  <v-app>
    <div>
      <v-app-bar dark dense>
<v-btn large flat to="/" exact>Gex</v-btn>

        <v-spacer></v-spacer>

        <v-menu left bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn icon v-bind="attrs" v-on="on">
              <v-icon>mdi-account</v-icon>
            </v-btn>
          </template>

          <v-list>
            <v-list-item-group v-model="selectedItem">
              <v-list-item
                v-for="(item, i) in items"
                :key="i"
                link
                :to="item.link"
              >
                <v-list-item-content>
                  <v-list-item-title v-text="item.text"></v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-menu>
      </v-app-bar>
    </div>
    <v-main>
      <!-- Provides the application the proper gutter -->
      <v-container>
        <!-- If using vue-router -->
        <router-view></router-view>
      </v-container>
    </v-main>

    <v-footer app>
      <!-- -->
    </v-footer>
  </v-app>
</template>

<script>
import HelloWorld from "./components/HelloWorld";

export default {
  name: "App",

  components: {
    HelloWorld,
  },

  data: () => ({
    selectedItem: 0,
    items: [
      { text: "Registrarse", link: "register" },
      { text: "Iniciar sesión", link: "login" },
    ],
  }),
};
</script>
