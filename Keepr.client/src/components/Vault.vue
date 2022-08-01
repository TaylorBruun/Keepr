<template>

    <!-- TODO get the picture to display on first visit to profile page -->
    <div @click="goToVault" :style="{ minHeight: variedHeight }" class="vault-card p-3 m-2">
        <h3>{{ vault.name }}</h3>
        <h6>{{ vault.description }}</h6>
        <img class="img-fluid" :src="firstPicture" alt="">

    </div>







</template>


<script>
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { computed, onMounted } from 'vue'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'

export default {
    props: { vault: { type: Object, required: true } },
    setup(props) {

        const router = useRouter();
        let variedHeight = heightWiggle();
        let firstPicture = keepsService.findVault(props.vault.id).firstPicture

        function heightWiggle() {
            return (Math.random() * 10 + 25).toFixed() + "vh"
        };

        onMounted(async () => {
            try {
                await keepsService.GetKeepsByVault(props.vault.id)
            } catch (error) {
                Pop.toast(error, "error")
                logger.error(error)
            }
        })
        
        return {
            variedHeight,
            firstPicture,
            goToVault() {
                router.push({ name: "Vault", params: { id: props.vault.id } })
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.vault-card {
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