<template>
    <main class="Scans">        
        <ul class="scan-list no-grow">
            <li v-for="scan in set.scans" :key="scan.id" class="scan">
                <h2>Scan by {{ scan.author.name }} on {{ scan.creationDate | format }}</h2>
                <div class="image-settings metadata">
                    <strong>Image Settings:</strong> 
                    Threshold: <span> {{ scan.threshold }}</span>,
                    Resolution: <span> {{ scan.resolution }}</span>,
                    Magnification: <span>{{ scan.magnification }}</span>
                </div>

                <div class="instrument metadata">
                    <strong>Instrument Used:</strong>
                    {{ scan.instrument.type.model }} 
                    {{ scan.instrument.type.version }} 
                    {{ scan.instrument.serialNo !== null ? `(Serial No: ${scan.instrument.serialNo})` : '' }}
                </div>

                <h3>Lands:</h3>
                <ul class="grid lands no-grow">
                    <li v-for="(landId, key) in scan.landIds" :key="landId">
                        <a v-bind:href="'api/lands/' + landId " class="fix3p" target="blank">{{ key + 1 }}</a>
                    </li>
                </ul>
            </li>
        </ul>
    </main>
</template>

<style lang="scss" scoped>
    @import "../../vars.scss";

    h2 {
        color: $primaryColor;
    }

    .row {
        div {
            display: inline-block;
            margin: 0 15px;

            &:first-of-type {
                margin-left: 0;
            }
        }
    }    

    .scan-list {
        margin: 0 auto;
        list-style-type: none;

        .scan {
            margin: 50px 0;

            .metadata {
                margin: 15px 0;
                
                strong {
                    display: inline-block;
                    min-width: 100px;
                }
            }

            .lands {
                margin: 15px 0;

                li {
                    flex-basis: calc(90% / 6);
                    font-size: 50px;
                    text-align: center;
                }
            }
        }
    }


</style>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    declare var require: any
    var _ = require('lodash');

    @Component({
        // @ts-ignore
        name: "scans",
        props: ["set"]
    })
    export default class Scans extends Vue {
        asyncData({ store, route }: DataParameters) {
        }
    }
</script>
