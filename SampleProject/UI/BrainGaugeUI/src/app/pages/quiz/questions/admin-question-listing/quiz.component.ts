import {  Component, HostListener, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';
import { Router } from '@angular/router';
import { PageAccessType, Roles, SortDirection, SortFields } from 'src/app/shared/enums';
import { ToastNotificationService } from 'src/app/services/toastr.service';
import { Question, Quiz, QuizConfig,Option } from 'src/app/entities/quiz';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss'],
})
export class QuizComponent extends BaseComponent  implements OnInit {

  public listQuiz: any = [];
  panelOpenState: boolean = false;
  public isFilterInProgress: boolean = false;
  public count: number;
  quizes: any[];
  quiz: Quiz = new Quiz(null);
  mode = "quiz";
  quizName: string;
  config: QuizConfig = {
    allowBack: true,
    allowReview: false,
    autoMove: true, // if true, it will move to next question automatically when answered.
    duration: 300, // indicates the time (in secs) in which quiz needs to be completed. 0 means unlimited.
    pageSize: 1,
    requiredAll: false, // indicates if you must answer all the questions before submitting.
    richText: false,
    shuffleQuestions: false,
    shuffleOptions: false,
    showClock: false,
    showPager: true,
    theme: "none"
  };

  pager = {
    index: 0,
    size: 1,
    count: 1
  };
  timer: any = null;
  startTime: Date;
  endTime: Date;
  ellapsedTime = "00:00";
  duration = "";
  constructor(
    protected _ctxService: ContextService,
    private quizService: QuizService,
    protected _router: Router,
    private toastService: ToastNotificationService  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
    this._pageAccessLevel.push(Roles.User);
    // this.generateQuiz();
  }

  ngOnInit() {
    super.ngOnInit();
    this.quizes = this.quizService.getAll();
    this.quizName = this.quizes[0].id;
    this.loadQuiz(this.quizName);

  }
  loadQuiz(quizName: string) {
    let getQuestion = this.quizService.get(quizName);
    this.quiz = new Quiz(getQuestion);
    this.pager.count = this.quiz.questions.length;
    this.startTime = new Date();
    this.ellapsedTime = "00:00";
    this.timer = setInterval(() => {
      this.tick();
    }, 1000);
    this.duration = this.parseTime(this.config.duration);
    this.mode = "quiz";
  }

  tick() {
    if(this.listQuiz.length > 0)
    {
      const now = new Date();
      const diff = (now.getTime() - this.startTime.getTime()) / 1000;
      if (diff >= this.config.duration) {
        this.onSubmit();
      }
      this.ellapsedTime = this.parseTime(diff);
  
    }
  }

  parseTime(totalSeconds: number) {
    let mins: string | number = Math.floor(totalSeconds / 60);
    let secs: string | number = Math.round(totalSeconds % 60);
    mins = (mins < 10 ? "0" : "") + mins;
    secs = (secs < 10 ? "0" : "") + secs;
    return `${mins}:${secs}`;
  }

  get filteredQuestions() {
    return this.listQuiz
      ? this.listQuiz.slice(
          this.pager.index,
          this.pager.index + this.pager.size
        )
      : [];
  }

  onSelect(question: Question, option: Option) {
    if (question.questionTypeId === 1) {
      question.options.forEach(x => {
        if (x.id !== option.id) x.selected = false;
      });
    }

    if (this.config.autoMove) {
      this.goTo(this.pager.index + 1);
    }
  }

  goTo(index: number) {
    if (index >= 0 && index < this.pager.count) {
      this.pager.index = index;
      this.mode = "quiz";
    }
  }

  isAnswered(question: any) {
    return  question.answer != null && question.answer.length > 0 ? "Answered" : "Not Answered";
  }

  isCorrect(question: Question) {
    return question.options.every(x => x.selected === x.isAnswer)
      ? "correct"
      : "wrong";
  }

  onSubmit() {
    console.log(this.listQuiz,"submit")
    this.isFilterInProgress = true;
    this.quizService.saveQuizResponse(this.listQuiz).subscribe((data: number) => {
      if(data == -1)
      {
        this.toastService.showError("Question should be unique.", "Question");
      }
      else
      {
        this.toastService.showSuccess("Quiz has been submitted successfully.", "Question");
      }
      this.router.navigate(['questions']);
      this.isFilterInProgress = false;
      
    })
    // let answers = [];
    // this.quiz.questions.forEach(x =>
    //   answers.push({
    //     quizId: this.quiz.id,
    //     questionId: x.id,
    //     answered: x.answered
    //   })
    // );

    // Post your data to the server here. answers contains the questionId and the users' answer.
    this.mode = "result";
  }
  @HostListener("window:focus", ["$event"])
  onFocus(event: any): void {
    //console.log("On Focus");
  }

  @HostListener("window:blur", ["$event"])
  onBlur(event: any): void {
    console.log("On Blur");
  }
  @HostListener("window:beforeunload", ["$event"])
  unloadNotification($event: any) {}
  generateQuiz() {
    this.isFilterInProgress = true;
    this.quizService.generateUserQuiz(0).subscribe((d: any) => {
      if(d?.data != null && d.status.code == 200)
      {
        this.listQuiz = d.data;
        this.pager.count = this.listQuiz.length;
      }
      this.isFilterInProgress = false;
    });
  }
}