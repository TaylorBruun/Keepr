<template>

    <!-- <div :style="{ backgroundImage: `url(${firstImgInVault})`, minHeight: variedHeight }" class="position-relative vault-card p-3 m-2"> -->
    <div :style="{minHeight: variedHeight}"  class="vault-card p-3 m-2">
        <h3>{{ vault.name }}</h3>
        <h6>{{ vault.description }}</h6>
        <img class="img-fluid" src="https://images.unsplash.com/photo-1577373644244-ff9935a13a2b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=742&q=80" alt="">

    </div>







</template>


<script>
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { onMounted } from 'vue'
import Pop from '../utils/Pop'

export default {
    props: { vault: { type: Object, required: true } },
    setup(props) {
        function heightWiggle() {
            return (Math.random() * 10 + 25).toFixed() + "vh"
        };
       
        let firstImgInVault = ''
        onMounted(async () => {
            try {
                 firstImgInVault = await keepsService.GetKeepsByVault(props.vault.id)
            } catch (error) {
                Pop.toast(error, "error")
                logger.error(error)
            }
        })

        const test = "https://thiscatdoesnotexist.com"

        const router = useRouter();
        let variedHeight = heightWiggle();
        return {
            variedHeight,
            firstImgInVault,
            test,
            goProfile() {
                logger.log('pushing with id', props.vault.creatorId)
                router.push({ name: "Profile", params: { id: props.vault.creatorId } })
            },

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