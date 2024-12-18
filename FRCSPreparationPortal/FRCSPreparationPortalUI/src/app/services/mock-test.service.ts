import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignUp, User } from '../entities/user';
import { Settings } from '../helpers/settings';

@Injectable({
    providedIn: 'root'
})
export class MockTestService {

    constructor(
        private _http: HttpClient
    ) {
    }

    generateUserMockTest(userId): Observable<any> {
        return this._http.get<any>(`${Settings.apiBase}mocktest/generateMockTest/${userId}`);
    }

    saveMockTestResponse(ans: any): Observable<number> {
        return this._http.post<number>(`${Settings.apiBase}mocktest/saveMockTestResponse`, ans)
    }


    get(type: string): any {
        let data: any;
        switch (type) {
          case "javascript":
            return JAVASCRIPT;
          case "aspnet":
            return JAVASCRIPT;
          case "csharp":
            return JAVASCRIPT;
          case "designPatterns":
            return JAVASCRIPT;
        }
      }
    
      getAll() {
        return [
          { id: "javascript", name: "JavaScript" },
          { id: "aspnet", name: "Asp.Net" },
          { id: "csharp", name: "C Sharp" },
          { id: "designPatterns", name: "Design Patterns" }
        ];
      }
    

}
export const JAVASCRIPT = {
    "id": 1,
    "name": "JavaScript Quiz",
    "description": "JavaScript Quiz (Basic Multiple Choice Questions for JavaScript Developers)",
    "questions": [
        {
            "id": 1010,
            "name": "Which HTML tag do we use to put the JavaScript?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "<javascript>",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "<script>",
                    "isAnswer": true
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "<js>",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "None of the above",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1011,
            "name": "Which built-in method calls a function for each element in the array?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "while()",
                    "isAnswer": false
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "loop",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "forEach",
                    "isAnswer": true
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "takeUntil",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1012,
            "name": "What is the difference between let and var?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "let has local scope",
                    "isAnswer": true
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "Both are same",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "var is new data type",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "let consumes more cpu and ram",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1013,
            "name": "What is TypeScript?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "A Language based on Javascript",
                    "isAnswer": true
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "script that runs on browser",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "A DataType Collection of Javascript",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "None of the above",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1014,
            "name": "Which of the following is right syntex for arrow function?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "a -> { return b; }",
                    "isAnswer": false
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "x <= x + y;",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "x <- x + 5;",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "x => x + 5;",
                    "isAnswer": true
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1015,
            "name": "Which new ES6 syntax helps with formatting output text - mixing variables with string literals, for example.",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "Generator Functions",
                    "isAnswer": false
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "Arrow Functions",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "Template Strings",
                    "isAnswer": true
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "Set Data Structure",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1016,
            "name": "Which ES6 feature helps in merging of a number of changed properties into an existing object?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "Class syntex",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "Object.assign()",
                    "isAnswer": true
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "map data structure",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "Array.includes(obj);",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1017,
            "name": "What is the difference between == and === ?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "=== throws syntex error",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "== checks values only, === checks types as well",
                    "isAnswer": true
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "=== is reference type check only",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "Both are same",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1018,
            "name": "Which of the following is NOT the method of an Array?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": ".map()",
                    "isAnswer": false
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": ".includes()",
                    "isAnswer": false
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": ".subscribe()",
                    "isAnswer": true
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": ".flatMap()",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        },
        {
            "id": 1019,
            "name": "What will be the output of the following code: ['a', 'b', 'c'].fill(7, 1, 2);?",
            "questionTypeId": 1,
            "options": [
                {
                    "id": 1055,
                    "questionId": 1010,
                    "name": "['a', 7, 'c']",
                    "isAnswer": true
                },
                {
                    "id": 1056,
                    "questionId": 1010,
                    "name": "['a', 7, 7, 'b', 'c']",
                    "isAnswer": false
                },
                {
                    "id": 1057,
                    "questionId": 1010,
                    "name": "['a', 'b', 'c']",
                    "isAnswer": false
                },
                {
                    "id": 1058,
                    "questionId": 1010,
                    "name": "['7', 7, 'c']",
                    "isAnswer": false
                }
            ],
            "questionType": {
                "id": 1,
                "name": "Multiple Choice",
                "isActive": true
            }
        }
    ]
}