<template>
    <div class="component">
        <div class="d-flex justify-content-between">

            <img class="m-4 profile-img" :src="profile.picture" alt="">
            <div>
                <h2 class="text-break m-4 display-2">
                    {{ profile.name }}
                </h2>
                <h5 class="m-4">Vaults: {{ profileVaults.length }} </h5>
                <h5 class="m-4">Keeps: {{ profileKeeps.length }} </h5>
            </div>
        </div>
        <div class="spacer"></div>

        <div>
            <h4 class="m-2 p-2"> Vaults:</h4>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <div class="masonry-frame">
                            <Vault v-for="v in profileVaults" :key="v.id" :vault="v" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="small-spacer"></div>

        <div class="">
            <h4 class="m-2 p-2"> Keeps:</h4>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <div class="masonry-frame">
                            <Keep v-for="k in profileKeeps" :key="k.id" :keep="k" class=""></Keep>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import Keep from '../components/Keep.vue'
import Vault from '../components/Vault.vue'

export default {
    setup() {
        const route = useRoute();
        onMounted(async () => {
            try {
                await profilesService.GetProfile(route.params.id);
                await profilesService.GetProfileKeeps(route.params.id);
                await profilesService.GetProfileVaults(route.params.id);
            }
            catch (error) {
                Pop.toast(error, "error");
                logger.error(error);
            }
        });
        return {
            profile: computed(() => AppState.currentProfile),
            profileKeeps: computed(() => AppState.currentProfileKeeps),
            profileVaults: computed(() => AppState.currentProfileVaults)
        };
    },
    components: { Keep, Vault }
}
</script>


<style lang="scss" scoped>
.profile-img {
    width: 200px;
    height: 200px;
    border-radius: 50%;

}

.spacer {
    height: 15vh;
}

.small-spacer {
    height: 5vh;
}

.masonry-frame {
    columns: 4;

    div {
        break-inside: avoid;
    }
}

@media (max-width: 768px) {
    .masonry-frame {
        columns: 2;

        div {
            break-inside: avoid;
        }
    }
}
</style>