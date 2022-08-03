<template>

    <div @click="setActive" :style="{ backgroundImage: `url(${keep.img})`, minHeight: variedHeight }"
        class="position-relative keep-card p-3 m-3">

        <h3>{{ keep.name }}</h3>
        <h6>Keeps: {{ keep.kept }}| Views: {{ keep.views }}</h6>
        <h6>{{ keep.description }}</h6>
        <img v-if="route.name != 'Profile'" @click.stop="goToProfile"
            class="m-2 creator-img position-absolute bottom-0 end-0" :src="keep.creator.picture" alt="">

    </div>


</template>


<script>
import { Modal } from 'bootstrap';
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState';
import { logger } from '../utils/Logger'

export default {
    props: { keep: { type: Object, required: true } },

    setup(props) {
        const route = useRoute();
        const router = useRouter();
        function heightWiggle() {
            return (Math.random() * 10 + 25).toFixed() + "vh"
        };
        let variedHeight = heightWiggle();
        return {
            route,
            variedHeight,
            goToProfile() {
                router.push({ name: "Profile", params: { id: props.keep.creatorId } })
            },
            setActive(){
                AppState.activeKeep = props.keep
                Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
            }

        }
    }
}
</script>


<style lang="scss" scoped>
@import "../assets/scss/variables";
.keep-card {
    background-color: rgb(224, 236, 238);
    color: rgb(5, 5, 5);
    text-shadow: 0px 0px 6px white;
    box-shadow: 0 3px 3px -2px rgba(0, 0, 0, 0.2), 0 3px 4px 0 rgba(0, 0, 0, 0.14), 0 1px 8px 0 rgba(0, 0, 0, 0.12);
    transition: all 0.1s linear;
    border-radius: 1.5%;

    overflow-x: hidden;
    background-size: cover;
    background-repeat: no-repeat;

}

.keep-card:hover {
    transform: scale(1.02);
    cursor: pointer;
    opacity: 0.9
}


.creator-img {
    width: 30px;
    height: 30px;
    border-radius: 50%;
}
</style>