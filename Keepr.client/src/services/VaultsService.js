import { useRouter } from "vue-router";
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

const router = useRouter();


class VaultsService {

    async getVaultById(id) {

        const res = await api.get(`api/vaults/${id}`)
        AppState.activeVault = res.data
        return res.data

    }   

    async deleteVault() {
        let id = AppState.activeVault.id
        const res = await api.delete(`api/vaults/${id}`)
        Pop.toast("Vault Deleted", 'success')
        router.push({ name: "Profile" })
        return res.data
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        if (AppState.account.id == AppState.currentProfile.id) {
            AppState.userVaults.unshift(res.data)
        } else {
            AppState.currentProfileVaults.unshift(res.data)
        }

        return res.data
    }

    async getUserVaults() {
        const res = await api.get('account/vaults')
        AppState.userVaults = res.data
    }

    async getFirstImagesDictionaryByProfile(profileId){
        const res = await api.get(`api/profiles/${profileId}/vaultsimgs`)
        // logger.log(res.data)
        AppState.firstImagesForVaults = res.data
    }
}

export const vaultsService = new VaultsService()