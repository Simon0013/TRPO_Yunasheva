--
-- PostgreSQL database dump
--

-- Dumped from database version 10.16
-- Dumped by pg_dump version 13.3

-- Started on 2021-12-18 11:20:53

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

--
-- TOC entry 203 (class 1259 OID 90772)
-- Name: attestation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.attestation (
    employee_id integer NOT NULL,
    attestation_date date NOT NULL,
    count_questions integer NOT NULL,
    count_answers integer NOT NULL,
    grade boolean NOT NULL,
    comiss_member1 integer NOT NULL,
    comiss_member2 integer NOT NULL,
    comiss_member3 integer NOT NULL,
    comiss_member4 integer NOT NULL,
    comiss_member5 integer NOT NULL
);


ALTER TABLE public.attestation OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 90692)
-- Name: company; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.company (
    id integer NOT NULL,
    name character varying(1000) NOT NULL,
    short_name character varying(300) NOT NULL
);


ALTER TABLE public.company OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 90700)
-- Name: doljnost; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.doljnost (
    id integer NOT NULL,
    name character varying(1000) NOT NULL,
    short_name character varying(300) NOT NULL
);


ALTER TABLE public.doljnost OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 90739)
-- Name: education; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.education (
    id integer NOT NULL,
    number integer NOT NULL,
    company_id integer NOT NULL,
    document character varying(5000) NOT NULL,
    qualification character varying(80) NOT NULL
);


ALTER TABLE public.education OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 90716)
-- Name: employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employee (
    id integer NOT NULL,
    fio character varying(1000) NOT NULL,
    birthday date NOT NULL,
    company_id integer NOT NULL,
    block character varying(500) NOT NULL,
    doljnost_id integer NOT NULL,
    prikaz_id integer NOT NULL,
    start_work_date date NOT NULL,
    end_work_date date NOT NULL,
    salary numeric(10,2) NOT NULL,
    start_experience date NOT NULL,
    start_special date NOT NULL
);


ALTER TABLE public.employee OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 90757)
-- Name: high_qualification; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.high_qualification (
    id integer NOT NULL,
    number integer NOT NULL,
    company_id integer NOT NULL,
    start_date date NOT NULL,
    end_date date NOT NULL
);


ALTER TABLE public.high_qualification OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 90708)
-- Name: prikaz; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.prikaz (
    id integer NOT NULL,
    create_date date NOT NULL,
    name character varying(1000) NOT NULL
);


ALTER TABLE public.prikaz OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 90684)
-- Name: school; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.school (
    id integer NOT NULL,
    name character varying(1000) NOT NULL,
    address character varying(2000) NOT NULL
);


ALTER TABLE public.school OWNER TO postgres;

--
-- TOC entry 2859 (class 0 OID 90772)
-- Dependencies: 203
-- Data for Name: attestation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.attestation (employee_id, attestation_date, count_questions, count_answers, grade, comiss_member1, comiss_member2, comiss_member3, comiss_member4, comiss_member5) FROM stdin;
1	2021-04-30	10	9	t	2	3	2	3	2
2	2020-10-25	10	10	t	3	3	3	3	3
3	2021-02-15	10	8	t	2	2	2	2	2
4	2021-12-16	10	10	t	3	3	3	3	3
\.


--
-- TOC entry 2853 (class 0 OID 90692)
-- Dependencies: 197
-- Data for Name: company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.company (id, name, short_name) FROM stdin;
2	ООО "КОРУС Консалтинг СНГ"	СберКорус
3	ПАО "Вымпел-Коммуникации"	ВымпелКом
4	ПАО "ПромСвязьБанк"	ПСБ
1	ПАО "Сбербанк"	Сбер
\.


--
-- TOC entry 2854 (class 0 OID 90700)
-- Dependencies: 198
-- Data for Name: doljnost; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.doljnost (id, name, short_name) FROM stdin;
1	Младший разработчик ПО	Программист
2	Руководитель отдела поддержки аутстафф-направления	Руководитель отдела
3	Руководитель направления "Цифровой бизнес"	Руководитель направления
\.


--
-- TOC entry 2857 (class 0 OID 90739)
-- Dependencies: 201
-- Data for Name: education; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.education (id, number, company_id, document, qualification) FROM stdin;
1	1	1	Диплом о среднем профессиональном образовании	Техник-программист
3	3	3	Диплом о высшем образовании	Инженер
4	4	4	Диплом о ВО	Инженер
\.


--
-- TOC entry 2856 (class 0 OID 90716)
-- Dependencies: 200
-- Data for Name: employee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employee (id, fio, birthday, company_id, block, doljnost_id, prikaz_id, start_work_date, end_work_date, salary, start_experience, start_special) FROM stdin;
1	Юнашева Екатерина Петрова	2001-01-12	1	Дирекция разработки программного обеспечения	1	1	2021-03-10	2024-03-09	45000.00	2021-03-10	2021-03-10
2	Иванов Пётр Сергеевич	1988-05-27	2	Дивизион "Социальный бизнес"	2	2	2017-02-03	2027-02-03	85000.00	2006-04-01	2006-04-01
3	Прекрасная Елена Михайловна	1984-01-20	3	Дивизион "Цифровой бизнес"	3	3	2021-06-13	2022-06-13	100000.00	2003-07-23	2005-05-26
4	Филатов Филипп Филиппович	1970-06-10	4	Руководитель отдела	2	3	2021-12-06	2022-12-06	110000.00	1989-01-06	1995-02-08
\.


--
-- TOC entry 2858 (class 0 OID 90757)
-- Dependencies: 202
-- Data for Name: high_qualification; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.high_qualification (id, number, company_id, start_date, end_date) FROM stdin;
1	1	1	2021-01-30	2021-04-30
2	2	2	2020-07-25	2020-10-25
3	3	3	2020-10-30	2021-02-15
\.


--
-- TOC entry 2855 (class 0 OID 90708)
-- Dependencies: 199
-- Data for Name: prikaz; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.prikaz (id, create_date, name) FROM stdin;
1	2021-03-09	Назначение на должность
2	2021-08-02	Принятие на работу
3	2021-11-15	Назначение на должность
\.


--
-- TOC entry 2852 (class 0 OID 90684)
-- Dependencies: 196
-- Data for Name: school; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.school (id, name, address) FROM stdin;
1	Ростовский-на-Дону колледж связи и информатики	г. Ростов-на-Дону, ул. Тургеневская, д. 10/6
3	Южный федеральный университет	г. Ростов-на-Дону, ул. Большая Садовая, д. 105/42
4	Ростовский-на-Дону государственный экономический университет	г. Ростов-на-Дону
5	Московский физико-технический институт	Московская область, г. Долгопрудный, пер. Институтский, д. 5
2	Московский государственный университет им. М.В.Ломоносова	г. Москва, мкр Ленинские горы, д. 1
\.


--
-- TOC entry 2717 (class 2606 OID 90776)
-- Name: attestation attestation_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT attestation_pk PRIMARY KEY (employee_id, attestation_date);


--
-- TOC entry 2705 (class 2606 OID 90699)
-- Name: company company_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_pk PRIMARY KEY (id);


--
-- TOC entry 2707 (class 2606 OID 90707)
-- Name: doljnost doljnost_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doljnost
    ADD CONSTRAINT doljnost_pk PRIMARY KEY (id);


--
-- TOC entry 2713 (class 2606 OID 90746)
-- Name: education education_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.education
    ADD CONSTRAINT education_pk PRIMARY KEY (id, number);


--
-- TOC entry 2711 (class 2606 OID 90723)
-- Name: employee employee_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_pk PRIMARY KEY (id);


--
-- TOC entry 2715 (class 2606 OID 90761)
-- Name: high_qualification high_qualification_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.high_qualification
    ADD CONSTRAINT high_qualification_pk PRIMARY KEY (id, number);


--
-- TOC entry 2709 (class 2606 OID 90715)
-- Name: prikaz prikaz_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prikaz
    ADD CONSTRAINT prikaz_pk PRIMARY KEY (id);


--
-- TOC entry 2703 (class 2606 OID 90691)
-- Name: school school_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.school
    ADD CONSTRAINT school_pk PRIMARY KEY (id);


--
-- TOC entry 2726 (class 2606 OID 90782)
-- Name: attestation cm1_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT cm1_attestation_fk FOREIGN KEY (comiss_member1) REFERENCES public.employee(id);


--
-- TOC entry 2727 (class 2606 OID 90787)
-- Name: attestation cm2_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT cm2_attestation_fk FOREIGN KEY (comiss_member2) REFERENCES public.employee(id);


--
-- TOC entry 2728 (class 2606 OID 90792)
-- Name: attestation cm3_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT cm3_attestation_fk FOREIGN KEY (comiss_member3) REFERENCES public.employee(id);


--
-- TOC entry 2729 (class 2606 OID 90797)
-- Name: attestation cm4_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT cm4_attestation_fk FOREIGN KEY (comiss_member4) REFERENCES public.employee(id);


--
-- TOC entry 2730 (class 2606 OID 90802)
-- Name: attestation cm5_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT cm5_attestation_fk FOREIGN KEY (comiss_member5) REFERENCES public.employee(id);


--
-- TOC entry 2721 (class 2606 OID 90747)
-- Name: education education_employee_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.education
    ADD CONSTRAINT education_employee_fk FOREIGN KEY (id) REFERENCES public.employee(id);


--
-- TOC entry 2722 (class 2606 OID 90752)
-- Name: education education_school_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.education
    ADD CONSTRAINT education_school_fk FOREIGN KEY (company_id) REFERENCES public.school(id);


--
-- TOC entry 2725 (class 2606 OID 90777)
-- Name: attestation employee_attestation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attestation
    ADD CONSTRAINT employee_attestation_fk FOREIGN KEY (employee_id) REFERENCES public.employee(id);


--
-- TOC entry 2718 (class 2606 OID 90724)
-- Name: employee employee_company_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_company_fk FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 2720 (class 2606 OID 90734)
-- Name: employee employee_doljnost_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_doljnost_fk FOREIGN KEY (doljnost_id) REFERENCES public.doljnost(id);


--
-- TOC entry 2719 (class 2606 OID 90729)
-- Name: employee employee_prikaz_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_prikaz_fk FOREIGN KEY (prikaz_id) REFERENCES public.prikaz(id);


--
-- TOC entry 2723 (class 2606 OID 90762)
-- Name: high_qualification high_employee_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.high_qualification
    ADD CONSTRAINT high_employee_fk FOREIGN KEY (id) REFERENCES public.employee(id);


--
-- TOC entry 2724 (class 2606 OID 90767)
-- Name: high_qualification high_school_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.high_qualification
    ADD CONSTRAINT high_school_fk FOREIGN KEY (company_id) REFERENCES public.school(id);


-- Completed on 2021-12-18 11:20:53

--
-- PostgreSQL database dump complete
--

