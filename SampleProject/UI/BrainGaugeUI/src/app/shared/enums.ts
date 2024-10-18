export enum SideMenu {
    Dashboard = 1,
    Admin = 12,
    User = 13,
    Questions = 2,
    QuestionsAssignment = 3,
    Quiz = 4,
    Categories = 5,
    MockTest = 6,
}

export enum Roles {
    Admin = 1,
    User = 2
}

export enum PageAccessType {
    PUBLIC = 0,
    PRIVATE = 1
}

export enum Pages {
    None = 0,
    Dashboard = 1,
    Questions = 2,
    QuestionsAssignment = 3,
    Quiz = 4,
    Categories = 5,
    MockTest = 6
}

export enum SortFields {
    CreateStamp = 1,
    Name = 2,
    FullName = 3,
    FirstName = 4,
    LastName = 5,
    Email = 6,
    StatusId = 7,
    AdminName = 8,
    LastLogin = 9,
    LicenseType = 10,
    TotalUsers = 11,
    TotalProjects = 12,
    Summary = 13,
    ImpactId = 14,
    Mitigation = 15,
    Title = 16,
    Description = 17,
    RAGStatusId = 18,
    IssueId = 19,
    Causes = 20,
    DateRaised = 21,
    CategoriesId = 22,
    ConsequencesId = 23,
    LikelihoodId = 24,
    PriorityId = 25,
    Comments = 26,
    UpdateStamp = 27,
    Organization = 28,

}

export enum SortDirection {
    Asc = 1,
    Desc = 2
}