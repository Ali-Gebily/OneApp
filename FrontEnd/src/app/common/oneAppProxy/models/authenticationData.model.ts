export class AuthenticationData
{ 
    isAuthenticated: boolean;
    username: string;
    access_token: string;
    token_type: string;
    expires_in: string;

constructor(){
this.isAuthenticated=false;
this.username=null;
this.access_token=null;
this.token_type=null;
this.expires_in=null;

}
}