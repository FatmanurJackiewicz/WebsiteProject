import{x as h,y as b,cu as d,B as m,C as v,E as c,cq as f,cC as u,M as g,i as t,ce as x}from"./index-06bb17da.js";const y=h({fixedHeader:Boolean,fixedFooter:Boolean,height:[Number,String],hover:Boolean,...b(),...d(),...m(),...v()},"v-table"),C=c()({name:"VTable",props:y(),setup(a,r){let{slots:e}=r;const{themeClasses:n}=f(a),{densityClasses:i}=u(a);return g(()=>t(a.tag,{class:["v-table",{"v-table--fixed-height":!!a.height,"v-table--fixed-header":a.fixedHeader,"v-table--fixed-footer":a.fixedFooter,"v-table--has-top":!!e.top,"v-table--has-bottom":!!e.bottom,"v-table--hover":a.hover},n.value,i.value,a.class],style:a.style},{default:()=>{var o,s,l;return[(o=e.top)==null?void 0:o.call(e),e.default?t("div",{class:"v-table__wrapper",style:{height:x(a.height)}},[t("table",null,[e.default()])]):(s=e.wrapper)==null?void 0:s.call(e),(l=e.bottom)==null?void 0:l.call(e)]}})),{}}});export{C as V,y as m};
