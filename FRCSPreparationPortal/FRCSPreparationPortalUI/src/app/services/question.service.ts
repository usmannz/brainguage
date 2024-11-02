import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pager } from '../entities/pager';
import { Settings } from '../helpers/settings';
import { Questions, QuestionsAssignment } from '../entities/questions';

@Injectable({
    providedIn: 'root'
})
export class QuestionService {

    constructor(
        private _http: HttpClient
    ) {
    }
    getAllQuestions(pagination: Pager): Observable<any> {
        return this._http.post<any>(`${Settings.apiBase}questions/getAllQuestions`, pagination);
    }

    getAllUserQuestions(pagination: Pager,userId: number): Observable<any> {
        return this._http.post<any>(`${Settings.apiBase}questions/getAllUsersQuestions/${userId}`, pagination);
    }

    getAllQuestionsDropDown(): Observable<Questions[]> {
        return this._http.get<any>(`${Settings.apiBase}questions/questiondropdown`);
    }

    insertQuestion(question: any): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}questions/savequestion`, question)
    }

    insertUserQuestions(question: QuestionsAssignment[]): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}questions/assignquestions`, question)
    }


    deleteQuestion(questionId: number): Observable<number> {
        return this._http.delete<number>(`${Settings.apiBase}questions/${questionId}`)     
    }

    questionByUserId(questionId: number): Observable<number> {
        return this._http.get<number>(`${Settings.apiBase}questions/${questionId}/questionByUserId`)     
    }
}