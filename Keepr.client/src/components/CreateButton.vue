<template>
    <div class="component">


    </div>
</template>


<script>
import { api } from '../services/AxiosService'

export default {
    setup(){
        return {
            async createVault(){
                await api.post('api/vaults', vaultData)
            }
        }
    }
}
</script>


<style lang="scss" scoped>

</style>