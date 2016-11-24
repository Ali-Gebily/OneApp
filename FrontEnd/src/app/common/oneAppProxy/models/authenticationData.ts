export class AuthenticationData
{ 
    isAuth: boolean;
    username: string;
    access_token: string;
    token_type: string;
    expires_in: string;

constructor(){
this.isAuth=false;
this.username=null;
this.access_token=null;
this.token_type=null;
this.expires_in=null;

}
}