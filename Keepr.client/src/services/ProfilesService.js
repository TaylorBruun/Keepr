import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
    async GetProfile(profileId){
        const res = await api.get(`api/profiles/${profileId}`)
        AppState.currentProfile = res.data
    }
    async GetProfileKeeps(profileId){
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.currentProfileKeeps = res.data
    }
    async GetProfileVaults(profileId){
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.currentProfileVaults = res.data
    }
}

export const profilesService = new ProfilesService()