import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultsService{
    async getVaultById(id){
        const res = await api.get(`api/vaults/${id}`)
        logger.log('here is the get vault by id res', res.data)
    }
}

export const vaultsService = new VaultsService()