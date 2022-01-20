import { AboutMe } from "./AboutMe";
import { Project } from "./project/Project";

export interface LandingPortfolio {
    AboutMe: AboutMe,
    Projects: Project[]
}