<template>
    <main class="set">
        <div class="set-head">
            <h1 v-html="name"></h1>
            <div class="row">
                <div class="set-creationDate">Created on <time>{{ set.creationDate | format }}</time></div> 
                <div class="set-lastScanDate">Last updated on <time>{{ set.lastScanDate | format }}</time></div>
                <div class="opt" v-if="$store.state.authenticated">
                    <button class="btn add-scan" @click="editModeOn=true">Add Scan</button>
                    <button class="btn del" @click="remove">Delete Set</button>
                </div>
            </div>
            
        </div>
        <div class="edit" v-if="editModeOn">
            <ScanForm></ScanForm>
            <button class="btn-scan save" @click="save">Save</button>
            <button class="btn-scan cancel" @click="cancel">Cancel</button>
        </div>
        
        <rundown v-if="set.subsets.length > 0" v-bind:set="set"></rundown>
        <scans v-if="set.scans.length > 0" v-bind:set="set"></scans>
    </main>
</template>

<style lang="scss">
    @import "../../vars.scss";
    .header {
        margin-left: 25px;
        margin-right: 25px;
        margin-top: 25px;
        border-bottom: 3px solid #ccc;
        padding: 0 0 10px;
    }
    .set {
        padding: 15px;

        .set-head {
            padding: 20px 0;
            border-bottom: 5px solid $primaryColor;

            h1 a {
                text-decoration: none;
                color: $primaryColor;
                cursor: pointer;

                &:hover {
                    text-decoration: underline;
                }
            }
        }
    }

    .row {
        width: 100%;

        .opt {
            float:right;
        }
        .del, .add-scan {
            background: $primaryColor;
            border-radius: 5px;
            height: 30px;
            width: 130px;
            padding: 5px;
            margin: 0 5px;
            text-align: center;
            border-style: solid;
            border-width:thin;   
        }
        .del:hover, .add-scan:hover {
            cursor: pointer;
        }
        }    

    .barrels {
        li {

            h3 {
                text-decoration: none;
            }

            list-style-type: none;
        }
    }

    .bullets {
        margin: 15px;

        li {
            font-size: 30px;
            text-align: center;
            font-weight: bold;
            line-height: 90px;
            flex-basis: 10%;
        }
    }
    
    .edit {
        background: #eee;
        border-radius: 5px;
        margin-top: 10px;
        padding-bottom: 10px;
        .save {
            margin-left: 25px;
        }
        .cancel {
            margin-left: 10px;
        }
    }

</style>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import ScanForm from "./scanForm.vue";
    import Rundown from "./rundown.vue";
    import Scans from "./scans.vue";

     declare var require: any;

    Vue.component('ScanForm', ScanForm);

    @Component({
        // @ts-ignore
        components: {
            Rundown,
            Scans
        }
    })
    export default class Set extends Vue {
        editModeOn = false;

        asyncData({ store, route }: DataParameters) {
            return store.dispatch("getSet", route.params.id);
        } 

        get set() {
            return this.$store.state.sets[this.$route.params.id];
        }

        get view() {
            return "bullet" in this.$route.params ? "rundown" : "scans";
        }

        get barrel() {
            return this.$route.params.barrel;
        }

        get bullet() {
            return this.$route.params.bullet;
        }


        get name() {
            let nodes = [] as { id: number; name: string; childPrefix: string|null; ignorePrefix: boolean }[];
            let current = this.set;

            do {
                nodes.unshift(current);
                current = current.parent;
            } while(current !== null);

            let name = "";
            for(let i = 0; i < nodes.length; i++) {
                let includePrefix = i !== 0 && nodes[i - 1].childPrefix !== null && !nodes[i].ignorePrefix;
                name += `<a href="sets/${nodes[i].id}">`;
                name += includePrefix ? `${nodes[i - 1].childPrefix} ${nodes[i].name}` : `${nodes[i].name}`;
                name += `</a>`;
                name += i !== nodes.length - 1 ? " - " : "";
            }

            return name;
        }

        remove(){
            this.$store.dispatch("removeSet", this.set.id).then(
                () => this.$router.push({ name: 'home' })
            );
        }
         
        save(){
            this.editModeOn = false;
            let scans = (this.$children[1] as any).getScans();
            
            for(var key in scans){
                if (scans.hasOwnProperty(key)) {
                    scans[key].setId = this.set.id;
                }
            }
            let scanSet = {
                scans: scans
            }

            this.$store.dispatch("addAllScans", scanSet).then(
                () => this.$store.dispatch("getRundown", this.set.id)
            )
        }
        cancel() {
            this.editModeOn = false;
        }
    }
</script>