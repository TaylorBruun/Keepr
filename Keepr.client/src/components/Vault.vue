<template>
    <!-- TODO get the picture to display on first visit to profile page -->
    <div @click="goToVault" :style="{ minHeight: variedHeight }" class="vault-card p-3 m-3">
        <h3>{{ vault.name }}</h3>
        <h6>{{ vault.description }}</h6>
        <img class="img-fluid" :src="firstImagesDictionary" alt="">
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'

export default {
    props: { vault: { type: Object, required: true } },
    setup(props) {
        const router = useRouter();
        let variedHeight = heightWiggle();
        
        function heightWiggle() {
            return (Math.random() * 10 + 25).toFixed() + "vh"
        };

        return {
            variedHeight,
            firstImagesDictionary: computed(()=> AppState.firstImagesForVaults[props.vault.id]),
            goToVault() {
                router.push({ name: "Vault", params: { id: props.vault.id } })
            },
        }
    }
}
</script>


<style lang="scss" scoped>
.vault-card {
    background-color: #f0fffa;
    color: rgb(5, 5, 5);
    text-shadow: 0px 0px 6px white;
    box-shadow: 0 3px 3px -2px rgba(0, 0, 0, 0.2), 0 3px 4px 0 rgba(0, 0, 0, 0.14), 0 1px 8px 0 rgba(0, 0, 0, 0.12);
    transition: all 0.1s linear;
    border-radius: 1.5%;
    overflow-x: hidden;
    background-size: cover;
    background-repeat: no-repeat;
}

.vault-card:hover {
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