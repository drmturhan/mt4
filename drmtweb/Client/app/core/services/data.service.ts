// CREDIT:
//  The vast majority of this code came right from Ben Nadel's post:
//  http://www.bennadel.com/blog/3047-creating-specialized-http-clients-in-angular-2-beta-8.htm
// Typescript version written by Sam Storie
// https://blog.sstorie.com/adapting-ben-nadels-apigateway-to-pure-typescript/
// My updates are mostly adapting it for Typescript:
//  1. Importing required modules
//  2. Adding type notations
//  3. Using the 'fat-arrow' syntax to properly scope in-line functions

import { Injectable, Inject } from '@angular/core';
import { Http, Response, RequestOptions, RequestMethod, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { UtilityService } from './utility.service';
import { DataServiceOptions } from './data-service-options';
import { ToasterService } from "angular2-toaster/src/toaster.service";

@Injectable()
export class DataService {


    //private baseListeUrl: string = 'api/listeler/';

    //private cinsiyetler: Array<IKeyValuePair>;
    //private observable: Observable<any>;

    //getcinsiyetler(): Observable<any> {
    //    console.log('getData called');
    //    if (this.cinsiyetler) {
    //        console.log('data already available');
    //        // if `data` is available just return it as `Observable`
    //        return Observable.of(this.cinsiyetler);
    //    } else if (this.observable) {
    //        console.log('request pending');
    //        // if `this.observable` is set then the request is in progress
    //        // return the `Observable` for the ongoing request
    //        return this.observable;
    //    } else {
    //        console.log('send new request');
    //        // create the request, store the `Observable` for subsequent subscribers
    //        return this.get(this.baseListeUrl + 'cinsiyetlistesi')
    //            .do(
    //            cinsiyetler => {
    //                this.cinsiyetler = cinsiyetler as Array<IKeyValuePair>;
    //                return this.observable;
    //            }
    //            )
    //            .share();

    //    }
    //}


    // Define the internal Subject we'll use to push the command count
    public pendingCommandsSubject = new Subject<number>();
    public pendingCommandCount = 0;

    // Provide the *public* Observable that clients can subscribe to
    public pendingCommands$: Observable<number>;

    constructor(public http: Http, public us: UtilityService, @Inject(ToasterService) private toasterService: ToasterService) {
        this.pendingCommands$ = this.pendingCommandsSubject.asObservable();
    }

    // I perform a GET request to the API, appending the given params
    // as URL search parameters. Returns a stream.
    public get(url: string, params?: any): Observable<Response> {
        const options = new DataServiceOptions();
        options.method = RequestMethod.Get;
        options.url = url;
        options.params = params;
        return this.request(options);
    }

    // I perform a POST request to the API. If both the params and data
    // are present, the params will be appended as URL search parameters
    // and the data will be serialized as a JSON payload. If only the
    // data is present, it will be serialized as a JSON payload. Returns
    // a stream.
    public post(url: string, data?: any, params?: any): Observable<Response> {
        if (!data) {
            data = params;
            params = {};
        }
        const options = new DataServiceOptions();
        options.method = RequestMethod.Post;
        options.url = url;
        options.params = params;
        options.data = data;
        return this.request(options);
    }

    public put(url: string, data?: any, params?: any): Observable<Response> {
        if (!data) {
            data = params;
            params = {};
        }
        const options = new DataServiceOptions();
        options.method = RequestMethod.Put;
        options.url = url;
        options.params = params;
        options.data = data;
        return this.request(options);
    }

    public delete(url: string): Observable<Response> {
        const options = new DataServiceOptions();
        options.method = RequestMethod.Delete;
        options.url = url;
        return this.request(options);
    }

    private request(options: DataServiceOptions): Observable<any> {
        options.method = (options.method || RequestMethod.Get);
        options.url = (options.url || '');
        options.headers = (options.headers || {});
        options.params = (options.params || {});
        options.data = (options.data || {});

        this.interpolateUrl(options);
        this.addXsrfToken(options);
        this.addContentType(options);
        this.addAuthToken(options);

        const requestOptions = new RequestOptions();
        requestOptions.method = options.method;
        requestOptions.url = options.url;
        requestOptions.headers = options.headers;
        requestOptions.search = this.buildUrlSearchParams(options.params);
        requestOptions.body = JSON.stringify(options.data);

        this.pendingCommandsSubject.next(++this.pendingCommandCount);

        const stream = this.http.request(options.url, requestOptions)
            .catch((error: any) => {
                this.handleErrors(error);
                return Observable.throw(error);
            })
            .map(this.unwrapHttpValue)
            .catch((error: any) => {
                return Observable.throw(this.unwrapHttpError(error));
            })
            .finally(() => {
                this.pendingCommandsSubject.next(--this.pendingCommandCount);
            });

        return stream;
    }

    private addContentType(options: DataServiceOptions): DataServiceOptions {
        // if (options.method !== RequestMethod.Get) {
        options.headers['Content-Type'] = 'application/json; charset=UTF-8';
        // }
        return options;
    }

    private addAuthToken(options: DataServiceOptions): DataServiceOptions {
        const authTokens = localStorage.getItem('auth-tokens');
        if (authTokens) {
            // tslint:disable-next-line:whitespace
            options.headers.Authorization = 'Bearer ' + JSON.parse((<any>authTokens)).access_token;
        }
        return options;
    }

    private extractValue(collection: any, key: string): any {
        const value = collection[key];
        delete (collection[key]);
        return value;
    }

    private addXsrfToken(options: DataServiceOptions): DataServiceOptions {
        const xsrfToken = this.getXsrfCookie();
        if (xsrfToken) {
            options.headers['X-XSRF-TOKEN'] = xsrfToken;
        }
        return options;
    }

    private getXsrfCookie(): string {
        const matches = document.cookie.match(/\bXSRF-TOKEN=([^\s;]+)/);
        try {
            return matches ? decodeURIComponent(matches[1]) : '';
        } catch (decodeError) {
            return '';
        }
    }

    // private addCors(options: DataServiceOptions): DataServiceOptions {
    //     options.headers['Access-Control-Allow-Origin'] = '*';
    //     return options;
    // }

    private buildUrlSearchParams(params: any): URLSearchParams {
        const searchParams = new URLSearchParams();
        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                searchParams.append(key, params[key]);
            }
        }
        return searchParams;
    }

    private interpolateUrl(options: DataServiceOptions): DataServiceOptions {
        options.url = options.url.replace(/:([a-zA-Z]+[\w-]*)/g, ($0, token) => {
            // Try to move matching token from the params collection.
            if (options.params.hasOwnProperty(token)) {
                return (this.extractValue(options.params, token));
            }
            // Try to move matching token from the data collection.
            if (options.data.hasOwnProperty(token)) {
                return (this.extractValue(options.data, token));
            }
            // If a matching value couldn't be found, just replace
            // the token with the empty string.
            return ('');
        });
        // Clean up any repeating slashes.
        options.url = options.url.replace(/\/{2,}/g, '/');
        // Clean up any trailing slashes.
        options.url = options.url.replace(/\/+$/g, '');

        return options;
    }

    private unwrapHttpError(error: any): any {
        try {
            return (error.json());
        } catch (jsonError) {
            return ({
                code: -1,
                message: 'An unexpected error occurred.'
            });
        }
    }
    private unwrapHttpValue(value: Response): any {
        return (value.json());
    }
    private handleErrors(error: any) {
        if (error.status === 401) {
            sessionStorage.clear();
            this.toasterService.popAsync('warning', 'Oturum süresi doldu', 'Lütfen tekrar sisteme giriş yapın');
            this.us.navigateToSignIn();
        } else if (error.status === 403) {
            // Forbidden
            this.toasterService.popAsync('error', 'Yetkiniz yok', 'Bu işlem yapmak için yetkiniz yok!');
            this.us.navigateToSignIn();
        }
    }
    public toQueryString(obj: any) {
        var parts = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }
        return parts.join('&');
    }
}
