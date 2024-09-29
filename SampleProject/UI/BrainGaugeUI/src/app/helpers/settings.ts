import { environment } from "src/environments/environment";

declare var jquery: any;
declare var $: any;
declare var window: any;

export class Settings {
    static apiBase = environment.apiUrl;
    // static successUrl = window.env.successUrl;
    // static cancelUrl = window.env.cancelUrl;
    // static returnUrl = window.env.returnUrl;
    static currentYear = (new Date()).getFullYear();
    // static supportEmail = window.env.supportEmail;
}