/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

export interface lineData{
    date: string,
    topic0: number,
    topic0Name: string,
    topic1: number,
    topic1Name: string,
    topic2: number,
    topic2Name: string,
    topic3: number,
    topic3Name: string,
    topic4: number,
    topic4Name: string,

    [key: string]: number | string;
}