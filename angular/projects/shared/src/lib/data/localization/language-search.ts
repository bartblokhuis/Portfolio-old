import { BaseSearchModel } from "../common/base-search-model";

export interface LanguageSearch extends BaseSearchModel {
    onlyShowPublished: boolean;
}