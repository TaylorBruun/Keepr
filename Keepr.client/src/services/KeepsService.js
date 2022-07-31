import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService{

 async GetAllKeeps(){
    const res = await api.get('api/keeps')
    AppState.homeKeeps = res.data
 }

 async GetKeepsByVault(vaultId){
   const res = await api.get(`api/vaults/${vaultId}/keeps`)
   logger.log('here is the result of get keeps by vault', res.data)
   return res.data
 }
}

export const keepsService = new KeepsService()