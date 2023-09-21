export interface Project {
    id: number,
    name: string,
    about: string,
    owner: string,
    readMe: string,
    numberOfStars: number,
}


export const renderProject = (project: Project) => {
    return(
        <div className="flex flex-row">
            <p className="flex mr-5"> name: {project.name}</p>
            <p className="flex mr-5"> about: {project.about} </p>
            <p className="flex mr-5"> number of stars: {project.numberOfStars} </p>
        </div>
    )
}