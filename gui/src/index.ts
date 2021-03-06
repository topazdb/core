import Vue, { VNode, ComponentOptions } from "vue";
import app from "./app.vue";
import createStore from "./store";
import router from "./router";
import createAuth from "./auth";
import moment from "moment";
import { config } from "../package.json";
import { sync } from "vuex-router-sync";

Vue.filter("format", (date: string, never?: string) => {
    if(date === null) return never || "never";
    else return moment(date).format(config.dateFormat)
});

export default async ({ cookies, env, apiBaseUrl }) => {
    let auth = createAuth({ cookies, env });
    let store = createStore({ auth, env, apiBaseUrl });
    let mode = "normal";

    Vue.prototype.$auth = auth;
    sync(store, router);

    try {
        await store.dispatch("getPopulatorStatus");

    } catch(exception) {
        mode = "maintenance";
    }

    return {
        router,
        store,
        mode,
        app: new Vue({
            router, 
            store,
            render: h => h(app) 
        } as unknown as ComponentOptions<Vue>)
    }
};  